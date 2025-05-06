using UnityEngine;

public class BackgroundSlide : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject[] backgrounds;
    [SerializeField] private int screenIndex = 1;
    private const int cameraPositionIndex = 1;
    private Vector3[] backgroundPositions;
    private Vector3 screenSize;

    [SerializeField] private float slideSpeed;

    [SerializeField] private bool isSliding;
    [SerializeField] private int slideDirection;

    [SerializeField] private bool isCurrent;

    private void Start()
    {
        screenSize = backgrounds[cameraPositionIndex].transform.localScale;
        
        backgroundPositions = new Vector3[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgroundPositions[i] = new Vector3 (mainCamera.transform.localPosition.x + (screenSize.x * (i - 1)), mainCamera.transform.localPosition.y);
            backgrounds[i].transform.localPosition = backgroundPositions[i];
        }

        RepositionBackground();
    }

    private void Update()
    {
        if (!isCurrent)
            return;

        if (!isSliding)
        {
            if (Input.GetKeyDown(KeyCode.A))
                slideDirection = -1;

            if (Input.GetKeyDown(KeyCode.D))
                slideDirection = 1;
        }

        if (slideDirection != 0)
            SlideBackgrounds(slideDirection);
    }
    
    // 배경 슬라이드
    private void SlideBackgrounds(int direction)
    {
        if (!isSliding)
            isSliding = true;
        
        int nextScreenIndex = screenIndex + direction;
        
        nextScreenIndex = (int)Mathf.Repeat(nextScreenIndex, backgrounds.Length);
            
        backgrounds[screenIndex].transform.localPosition = Vector3.Lerp(backgrounds[screenIndex].transform.localPosition, backgroundPositions[cameraPositionIndex - direction], Time.deltaTime * slideSpeed);
        backgrounds[nextScreenIndex].transform.localPosition = Vector3.Lerp(backgrounds[nextScreenIndex].transform.localPosition, backgroundPositions[cameraPositionIndex], Time.deltaTime * slideSpeed);
        
        // 배경들을 맞는 위치로 이동
        if (Vector3.Distance(backgrounds[nextScreenIndex].transform.localPosition, backgroundPositions[cameraPositionIndex]) < 0.1f) // 목표 위치에 근접할 시 이동 완료한 것으로 판정
        {
            screenIndex = nextScreenIndex;
            isSliding = false;
            slideDirection = 0;

            RepositionBackground();
        }
    }

    private void RepositionBackground()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            int si = screenIndex + i;

            si = (int)Mathf.Repeat(si, backgrounds.Length);

            int positionIndex = cameraPositionIndex + i;

            if (positionIndex >= backgrounds.Length)
                positionIndex = positionIndex - backgrounds.Length;
            if (positionIndex < 0)
                positionIndex = positionIndex + backgrounds.Length;

            backgrounds[si].transform.localPosition = backgroundPositions[positionIndex];
        }
    }
}
