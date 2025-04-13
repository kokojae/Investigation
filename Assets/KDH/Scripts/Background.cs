using UnityEngine;

public class Background : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        transform.localScale = new Vector2(18, 10);
        
    }

    // 배경 사이즈를 화면 크기에 맞추는 코드(사용 안함)
    private void ResizeToScreen()
    {
        float spriteX = spriteRenderer.sprite.bounds.size.x;
        float spriteY = spriteRenderer.sprite.bounds.size.y;
        
        float screenY = Camera.main.orthographicSize * 2;
        float screenX = screenY / Screen.height * Screen.width;
        
        transform.localScale = new Vector2(Mathf.Ceil(screenX / spriteX), Mathf.Ceil(screenY / spriteY));
    }
}
