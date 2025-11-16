using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUi;
    [SerializeField] private GameObject gameWinUi;
    bool isGameOver = false;
    bool isGameWin = false;
    void Start()
    {
        gameOverUi.SetActive(false);
        gameWinUi.SetActive(false);
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
