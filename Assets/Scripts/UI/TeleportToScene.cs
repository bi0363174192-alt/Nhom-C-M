using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToScene : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;
    public string sceneName;      // Tên scene sẽ chuyển đến
    public Vector2 spawnPoint;    // Vị trí xuất hiện ở scene mới


    // tao bien scene name
    private const string Map2 = "Map2";
    private const string Map3 = "Map3";
    private const string MapBoss = "MapBoss";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Lưu vị trí spawn lại để scene sau đọc
            PlayerPrefs.SetFloat("SpawnX", spawnPoint.x);
            PlayerPrefs.SetFloat("SpawnY", spawnPoint.y);


            AudioManager.instance.StopBGM();
            SceneManager.LoadScene(sceneName);
            switch (sceneName)
            {
                case Map2:
                    AudioManager.instance.StartBGM("playlist_level1");
                    break;
                case Map3:
                    AudioManager.instance.StartBGM("playlist_level2");
                    break;
                case MapBoss:
                    AudioManager.instance.StartBGM("playlist_bosslevel");
                    break;
            }
        }
    }
}