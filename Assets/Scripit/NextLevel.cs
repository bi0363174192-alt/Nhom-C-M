using UnityEngine;
using UnityEngine.SceneManagement;

public class Nextlevel : MonoBehaviour
{
    [SerializeField] private string tenManChoiMoi = "Map2"; // Đặt tên cảnh kế tiếp trong Inspector

    public void LoadManChoiMoi()
    {
        Debug.Log("Đang load màn: " + tenManChoiMoi);
        SceneManager.LoadScene(tenManChoiMoi);
    }
}
