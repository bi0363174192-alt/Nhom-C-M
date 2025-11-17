using UnityEngine;

public class PlayerCollection : MonoBehaviour
{
    private GameManager gameManager;
    private AudioManager audioManager;
    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        audioManager = FindAnyObjectByType<AudioManager>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
 
        if (collision.CompareTag("Trap"))
        {
            gameManager.GameOver();
        }
        
        //else if (collision.CompareTag("Enemy"))
        //{
        //    gameManager.GameOver();
        //}
        //else if (collision.CompareTag("Key"))
        //{
        //    gameManager.GameWin();
        //}
        //else if (collision.CompareTag("Map"))
        //{
        //    gameManager.GameWin();
        //}
    }
}
