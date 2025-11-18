using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    [SerializeField] private AudioClip clickClip; // âm thanh click vào button
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        AudioManager.instance.StartBGM("playlist_mainMenu");
    }
    public void PlayGame()
    {
        audioSource.PlayOneShot(clickClip);

        //Tải màn hình LOADING
        MySceneManager.LoadSceneWithLoading("Map2");
    }

    // chuyển hướng tới cài đặt game? (dự định)
    public void SettingGame()
    {
        audioSource.PlayOneShot(clickClip);
        MySceneManager.LoadSceneWithLoading("Setting");
    }

    //url cho chuyển hướng bên ngoài, thêm liên kết bên dưới OnClick() của Button (Inspector trong unity)
    public void OpenLink(string url)
    {
        if (!string.IsNullOrEmpty(url))
        {
            Application.OpenURL(url);
            Debug.Log("Opening URL: " + url);
        }
        else
        {
            Debug.LogWarning("URL provided is empty!");
        }
    }
    

    public void QuitGame()
    {
        audioSource.PlayOneShot(clickClip);
        Application.Quit();
    }
}
