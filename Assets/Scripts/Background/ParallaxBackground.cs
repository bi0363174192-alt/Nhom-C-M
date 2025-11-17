using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Camera mainCamera;
    private float lastCameraPositionX;
    private float lastCameraPositionY; // <-- THÊM MỚI
    private float cameraHalfWidth;

    [SerializeField] private ParallaxLayer[] backgroundLayers;

    private void Awake()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Không tìm thấy Camera với Tag 'MainCamera'!");
            this.enabled = false; // Tắt script nếu không có camera
            return;
        }

        // THÊM MỚI: Khởi tạo vị trí Y ban đầu
        lastCameraPositionX = mainCamera.transform.position.x;
        lastCameraPositionY = mainCamera.transform.position.y;

        cameraHalfWidth = mainCamera.orthographicSize * mainCamera.aspect;
        InitializeLayers();
    }

    private void FixedUpdate()
    {
        // Tính toán cho trục X
        float currentCameraPositionX = mainCamera.transform.position.x;
        float distanceToMoveX = currentCameraPositionX - lastCameraPositionX; // Đổi tên
        lastCameraPositionX = currentCameraPositionX;

        // THÊM MỚI: Tính toán cho trục Y
        float currentCameraPositionY = mainCamera.transform.position.y;
        float distanceToMoveY = currentCameraPositionY - lastCameraPositionY;
        lastCameraPositionY = currentCameraPositionY;

        // Tính toán cạnh camera
        float cameraLeftEdge = currentCameraPositionX - cameraHalfWidth;
        float cameraRightEdge = currentCameraPositionX + cameraHalfWidth;

        foreach (ParallaxLayer layer in backgroundLayers)
        {
            // THAY ĐỔI: Truyền cả hai giá trị X và Y
            layer.Move(distanceToMoveX, distanceToMoveY);
            layer.LoopBackground(cameraLeftEdge, cameraRightEdge);
        }
    }

    private void InitializeLayers()
    {
        foreach (ParallaxLayer layer in backgroundLayers)
            layer.CalculateImageWidth();
    }
}