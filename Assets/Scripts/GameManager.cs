using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverUi;
    [SerializeField] private GameObject gameWinUi;
    bool isGameOver = false;
    bool isGameWin = false;
    void Start()
    {
        UpdateScore();
        gameOverUi.SetActive(false);
        gameWinUi.SetActive(false);
    }

    //public void AddScore(int points)
    //{
    //    if (!isGameOver&&!isGameWin)
    //    {
    //        score += points;
    //        UpdateScore();
    //    }
    //}
    private void UpdateScore()
    {
        scoreText.text = score.ToString();
    }
    public void GameOver()
    {
        isGameOver = true;
        score = 0;
        Time.timeScale = 0;
        gameOverUi.SetActive(true);
    }
    public void RestartGame() { isGameOver = false; score = 0; UpdateScore(); Time.timeScale = 1; SceneManager.LoadScene("Game"); }

    public bool IsGameOver()
    {
        return isGameOver;
    }
    public void GameWin()
    {
        isGameWin = true;
        score = 0;
        Time.timeScale = 0;
        gameWinUi.SetActive(true);
    }
    public bool IsGameWin()
    {
        return isGameWin;
    }
    public void GotoMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
