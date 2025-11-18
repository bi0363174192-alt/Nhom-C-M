// Change the return type of FadeVolumeCo and SwitchMusicCo from IEnumerable to IEnumerator
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    [SerializeField] private AudioDatabaseSO audioDB;
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;
    [Space]

    private AudioClip lastMusicPlayed;
    private string currentBgmGroupName;
    private Coroutine currentBgmCo;
    [SerializeField] private bool bgmShouldPlay;

    private void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }


    private void Update()
    {
        if (bgmSource.isPlaying == false && bgmShouldPlay)
        {
            if (string.IsNullOrEmpty(currentBgmGroupName) == false)
                NextBGM(currentBgmGroupName);

        }

        if (bgmSource.isPlaying && bgmShouldPlay == false)
            StopBGM();
    }
    public void StartBGM(string musicGroup)
    {
        bgmShouldPlay = true;

        if (musicGroup == currentBgmGroupName)
            return;
        NextBGM(musicGroup);
    }
    public void NextBGM(string musicGroup)
    {
        bgmShouldPlay = true;

        currentBgmGroupName = musicGroup;
        if (currentBgmCo != null)
            StopCoroutine(currentBgmCo);
        currentBgmCo = StartCoroutine(SwitchMusicCo(musicGroup));
    }

    public void StopBGM()
    {
        bgmShouldPlay = false;

        StartCoroutine(FadeVolumeCo(bgmSource, 0, 1f));

        if (currentBgmCo != null)
            StopCoroutine(currentBgmCo);
    }
    private IEnumerator FadeVolumeCo(AudioSource source, float targetVolume, float duration)
    {
        float time = 0;
        float startVolume = source.volume;

        while (time < duration)
        {
            time += Time.deltaTime;

            source.volume = Mathf.Lerp(startVolume, targetVolume, time / duration);
            yield return null;
        }

        source.volume = targetVolume;
    }
    private IEnumerator SwitchMusicCo(string musicGroup)
    {
        AudioClipData data = audioDB.Get(musicGroup);


        if (data == null || data.clips.Count == 0)
        {
            Debug.Log("No audio found or group" + musicGroup);
            yield break;
        }
        AudioClip nextMusic = data.GetRandomClip();
        if (data.clips.Count > 1)
        {
            while (nextMusic == lastMusicPlayed)
            {
                nextMusic = data.GetRandomClip();
            }
        }
        if (bgmSource.isPlaying)
            yield return FadeVolumeCo(bgmSource, 0, 1f);

        lastMusicPlayed = nextMusic;
        bgmSource.clip = nextMusic;
        bgmSource.volume = 0;
        bgmSource.Play();

        StartCoroutine(FadeVolumeCo(bgmSource, data.maxVolume, 1f));
    }
    public void PlaySFX(string soundName, AudioSource sfxSource)
    {
        var data = audioDB.Get(soundName);
        if (data == null)
        {
            Debug.Log("Attemp to play sound: " + soundName);
            return;
        }

        var clip = data.GetRandomClip();
        if (clip == null)
            return;



        sfxSource.pitch = Random.Range(.95f, 1.1f);
        sfxSource.volume = data.maxVolume;
        sfxSource.clip = clip;
        sfxSource.PlayOneShot(clip);
    }
}