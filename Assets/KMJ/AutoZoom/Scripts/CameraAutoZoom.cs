using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraAutoZoom : MonoBehaviour
{
    [Tooltip("바운드를 딱 맞추는 기준 배율 (1 = 딱 맞춤, 1.1 = 10% 여백)")]
    [Range(1f, 2f)]
    public float paddingFactor = 1.1f;

    private Camera _cam;
    private Vector3 _initialPosition;
    private float _initialsize;
    private bool isZoom = false;

    public int CurrentZoomObjectID
    {
        get { return currentZoomObjectID; }
        private set { currentZoomObjectID = value; }
    }
    public int currentZoomObjectID = -1;

    private SceneObject zoomObject = null;

    void Awake()
    {
        _cam = GetComponent<Camera>();

        // 최초 카메라 위치 저장
        _initialPosition = _cam.transform.position;
        _initialsize = _cam.orthographicSize;
    }

    void Update()
    {
        // 현재 줌하지 않은 상태에서 왼쪽 클릭 → 바운드에 맞춰 줌
        if (!isZoom && Input.GetMouseButtonDown(0))
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                zoomObject = hit.collider.GetComponent<SceneObject>();
                if (zoomObject != null)
                {
                    FocusBounds(hit.collider.bounds);
                    zoomObject.ObjectZoom();
                }
                else
                {
                    Debug.LogError("줌된 오브젝트가 SceneObject가 아님!");
                }
            }
        }

        // 현재 줌이 되어있는 상태에서 오른쪽 클릭 → 최초 위치로 복귀
        if (isZoom && Input.GetMouseButtonDown(1))
        {
            BackOriginPosition();
            zoomObject.ObjectUnZoom();
            zoomObject = null;
        }
    }

    private void FocusBounds(Bounds b)
    {
        // 1) 카메라 위치: 바운드 중심(x,y)으로 이동, z는 유지
        Vector3 center = b.center;
        float z = _cam.transform.position.z;
        _cam.transform.position = new Vector3(center.x, center.y, z);

        // 2) orthographicSize 계산
        float aspect = (float)_cam.pixelWidth / _cam.pixelHeight;
        float halfHeight = b.extents.y;
        float halfWidthInHeight = b.extents.x / aspect;
        float requiredSize = Mathf.Max(halfHeight, halfWidthInHeight) * paddingFactor;
        _cam.orthographicSize = requiredSize;

        isZoom = true;
    }

    public void BackOriginPosition()
    {
        _cam.transform.position = _initialPosition;
        _cam.orthographicSize = _initialsize;

        isZoom = false;
    }
}
