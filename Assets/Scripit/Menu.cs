using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    [SerializeField] private AudioClip clickClip; // âm thanh click vào button
    [SerializeField] private AudioSource audioSource;

    public void PlayGame()
    {
        audioSource.PlayOneShot(clickClip);
        SceneManager.LoadScene("Game");
    }

    // chuyển hướng tới cài đặt game? (dự định)
    public void SettingGame()
    {
        audioSource.PlayOneShot(clickClip);
        SceneManager.LoadScene("Setting");
    }
    

    public void QuitGame()
    {
        audioSource.PlayOneShot(clickClip);
        Application.Quit();
    }
}
