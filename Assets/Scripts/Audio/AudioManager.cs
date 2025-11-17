using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    [SerializeField] private AudioDatabaseSO audioDB;
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;


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
        sfxSource.volume = data.volume;
        sfxSource.clip = clip;
        sfxSource.PlayOneShot(clip);
     } 
}
 