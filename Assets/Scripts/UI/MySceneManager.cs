using UnityEngine.SceneManagement; // Cần để dùng LoadScene

public static class MySceneManager
{
    // Đây là biến static để lưu tên scene mà chúng ta thực sự muốn tải SAU KHI LoadingScene hiển thị
    public static string SceneToLoad_Name; 

    // Hàm public này sẽ được gọi từ BẤT KỲ ĐÂU để bắt đầu quá trình tải
    public static void LoadSceneWithLoading(string nameOfSceneToLoad)
    {
        SceneToLoad_Name = nameOfSceneToLoad; // Lưu tên scene đích
        SceneManager.LoadScene("LoadingScene"); // Tải LoadingScene đầu tiên
    }
}