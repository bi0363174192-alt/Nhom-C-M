using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToScene : MonoBehaviour
{
    public string sceneName;      // Tên scene sẽ chuyển đến
    public Vector2 spawnPoint;    // Vị trí xuất hiện ở scene mới

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Lưu vị trí spawn lại để scene sau đọc
            PlayerPrefs.SetFloat("SpawnX", spawnPoint.x);
            PlayerPrefs.SetFloat("SpawnY", spawnPoint.y);

            SceneManager.LoadScene(sceneName);
        }
    }
}