using UnityEngine;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
    [SerializeField] private AudioClip clickClip; // âm thanh click vào button
    [SerializeField] private AudioSource audioSource;

    public void BackMenuGame()
    {
        audioSource.PlayOneShot(clickClip);
        SceneManager.LoadScene("Menu");
    }
}
