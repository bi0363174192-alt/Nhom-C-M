using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingManager : MonoBehaviour
{
    // Kéo Slider và Text từ Hierarchy vào đây trong Inspector
    public Slider loadingSlider;
    public TextMeshProUGUI progressText;

    void Start()
    {
        // Bắt đầu quá trình tải scene trong nền
        // Chúng ta gọi Coroutine để nó có thể chạy song song
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        // Lấy tên scene đã được lưu bởi "Người vận chuyển"
        string sceneToLoad = MySceneManager.SceneToLoad_Name;

        // Nếu lỡ quên (ví dụ test scene trực tiếp)
        if (string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.LogError("Không tìm thấy scene để tải. Quay về Menu.");
            SceneManager.LoadScene("Menu"); // Thay bằng tên Menu Scene của bạn
            yield break; // Dừng Coroutine
        }

        // Bắt đầu tải scene trong nền
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);

        // Vòng lặp while này sẽ chạy mỗi frame cho đến khi scene tải xong
        while (!operation.isDone)
        {
            // operation.progress đi từ 0.0 đến 0.9 (khi tải xong)
            // Chúng ta cần chuyển nó thành 0.0 đến 1.0
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            // 1. Cập nhật giá trị của Slider (0 đến 1)
            loadingSlider.value = progressValue;

            // 2. Cập nhật Text (nhân 100 để ra %)
            // "F0" nghĩa là không lấy số thập phân (ví dụ: 85%)
            progressText.text = "Loading... " + (progressValue * 100f).ToString("F0") + "__ 100";

            // Chờ đến frame tiếp theo rồi mới lặp lại
            yield return null;
        }

        // Khi operation.isDone = true, scene đã tải xong và tự động kích hoạt
    }
}