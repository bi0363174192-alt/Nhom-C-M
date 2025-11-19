using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUi;
    [SerializeField] private GameObject gameWinUi;
    [SerializeField] private GameObject pausePanel;
    bool isGameOver = false;
    bool isGameWin = false;
    void Start()
    {
        gameOverUi.SetActive(false);
        gameWinUi.SetActive(false);
        pausePanel.SetActive(false);
    }



    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0;
        gameOverUi.SetActive(true);
    }
    public void RestartGame() 
    {
        isGameOver = false;  
        Time.timeScale = 1; 
        MySceneManager.LoadSceneWithLoading(SceneManager.GetActiveScene().name);
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }
    public void GameWin()
    {
        isGameWin = true;
        Time.timeScale = 0;
        gameWinUi.SetActive(true);
    }
    public void PauseGame()
    {
        if(Time.timeScale == 1f)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public bool IsGameWin()
    {
        return isGameWin;
    }
    public void GotoMenu()
    {
        MySceneManager.LoadSceneWithLoading("Menu");
        Time.timeScale = 1;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
