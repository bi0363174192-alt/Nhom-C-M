using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
    [SerializeField] private Transform background;
    // THAY ĐỔI: Chuyển từ float sang Vector2 để có tốc độ X và Y riêng
    [SerializeField] private Vector2 parallaxMultiplier;
    [SerializeField] private float imageWidthOffset = 10;

    private float imageFullWidth;
    private float imageHalfWidth;

    public void CalculateImageWidth()
    {
        if (background == null)
        {
            Debug.LogError("LỖI PARALLAX: Một ParallaxLayer chưa được gán 'Background' (Transform) trong Inspector!");
            return;
        }

        SpriteRenderer sr = background.GetComponent<SpriteRenderer>();
        if (sr == null)
        {
            Debug.LogError("LỖI PARALLAX: GameObject '" + background.name + "' không có component SpriteRenderer!");
            return;
        }

        imageFullWidth = sr.bounds.size.x;
        imageHalfWidth = imageFullWidth / 2;
    }

    // THAY ĐỔI: Hàm Move giờ nhận cả distanceX và distanceY
    public void Move(float distanceToMoveX, float distanceToMoveY)
    {
        if (background == null) return; // Kiểm tra an toàn

        // Tính toán di chuyển riêng cho X và Y
        float moveX = distanceToMoveX * parallaxMultiplier.x;
        float moveY = distanceToMoveY * parallaxMultiplier.y;

        // Áp dụng di chuyển
        background.position += new Vector3(moveX, moveY, 0);
    }

    public void LoopBackground(float cameraLefteEdge, float cameraRightEdge)
    {
        if (background == null) return; // Kiểm tra an toàn

        float imageRightEdge = (background.position.x + imageHalfWidth) - imageWidthOffset;
        float imageLeftEdge = (background.position.x - imageHalfWidth) + imageWidthOffset;

        if (imageRightEdge < cameraLefteEdge)
            background.position += Vector3.right * imageFullWidth;
        else if (imageLeftEdge > cameraRightEdge)
            background.position += Vector3.right * -imageFullWidth;
    }
}