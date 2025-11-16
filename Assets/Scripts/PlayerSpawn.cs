using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    void Start()
    {
        // Lấy vị trí spawn đã lưu
        float x = PlayerPrefs.GetFloat("SpawnX", transform.position.x);
        float y = PlayerPrefs.GetFloat("SpawnY", transform.position.y);

        transform.position = new Vector2(x, y);
    }
}