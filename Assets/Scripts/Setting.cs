using UnityEngine;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
    [SerializeField] private AudioClip clickClip; // âm thanh click vào button
    [SerializeField] private AudioSource audioSource;
    private int Map_i = 1;

    public GameObject mapImage1;
    public GameObject mapImage2;
    public GameObject mapImage3;
    public GameObject mapImage4;


    // Hàm Start() để thiết lập trạng thái ban đầu khi Scene bắt đầu
    void Start()
    {
        // Khi Scene bắt đầu, chúng ta cần hiển thị đúng map_i hiện tại
        // và ẩn các map còn lại.
        // Hàm này sẽ tự động gọi DisplayCurrentMap() lần đầu.
        DisplayCurrentMap(); 
    }

    // Hàm này sẽ hiển thị map tương ứng với Map_i và ẩn các map khác
    void DisplayCurrentMap()
    {
        // Tắt tất cả các ảnh trước
        mapImage1.SetActive(false);
        mapImage2.SetActive(false);
        mapImage3.SetActive(false);
        mapImage4.SetActive(false);

        // Bật ảnh tương ứng với Map_i
        switch (Map_i)
        {
            case 1:
                mapImage1.SetActive(true);
                break;
            case 2:
                mapImage2.SetActive(true);
                break;
            case 3:
                mapImage3.SetActive(true);
                break;
            case 4:
                mapImage4.SetActive(true);
                break;
            default:
                Debug.LogWarning("Map_i có giá trị không hợp lệ: " + Map_i);
                // Đảm bảo có ít nhất một map được hiển thị nếu có lỗi
                mapImage1.SetActive(true); 
                break;
        }
    }

    // Hàm cho nút "Left"
    public void OnClick_LeftButton()
    {
        audioSource.PlayOneShot(clickClip);
        Map_i--; // Giảm chỉ số map
        if (Map_i < 1) // Nếu về dưới 1, quay lại 4
        {
            Map_i = 4;
        }
        DisplayCurrentMap(); // Cập nhật hiển thị map
        Debug.Log("Current Map: " + Map_i); // Xem trong Console để debug
    }

    // Hàm cho nút "Right"
    public void OnClick_RightButton()
    {
        audioSource.PlayOneShot(clickClip);
        Map_i++; // Tăng chỉ số map
        if (Map_i > 4) // Nếu vượt quá 4, quay lại 1
        {
            Map_i = 1;
        }
        DisplayCurrentMap(); // Cập nhật hiển thị map
        Debug.Log("Current Map: " + Map_i); // Xem trong Console để debug
    }

    public void GoToMap_i()
    {
        string sceneToLoad = ""; // Biến để lưu tên Scene cần load

        switch (Map_i)
        {
            case 1:
                sceneToLoad = "Game";
                break;
            case 2:
                sceneToLoad = "Map2";
                break;
            case 3:
                sceneToLoad = "Map3";
                break;
            case 4:
                sceneToLoad = "MapBoss";
                break;
            default:
                Debug.LogError("Không thể tải map. Map_i có giá trị không hợp lệ: " + Map_i);
                return; // Dừng hàm nếu có lỗi
        }

        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            // Tải màn hình LOADING (thay vì tải map trực tiếp)
            MySceneManager.LoadSceneWithLoading(sceneToLoad);
        }
        else
        {
            Debug.LogError("Tên Scene cần tải bị rỗng!");
        }
    }

    public void BackMenuGame()
    {
        audioSource.PlayOneShot(clickClip);
        MySceneManager.LoadSceneWithLoading("Menu");
    }
    
}
