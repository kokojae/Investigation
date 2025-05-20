using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    public Transform target; // 관찰할 대상
    public float distance = 5.0f; // 대상과 카메라 사이 거리
    public float xSpeed = 120.0f; // 마우스 X 회전 속도
    public float ySpeed = 120.0f; // 마우스 Y 회전 속도

    public float yMinLimit = -20f; // 위로 회전 제한
    public float yMaxLimit = 80f; // 아래로 회전 제한

    private float x = 0.0f;
    private float y = 0.0f;
    
    private bool isActive = false;

    void Start()
    {
        
    }

    public void ResetTransform()
    {
        isActive = true;
        transform.position = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
    }

    public void UnActive()
    {
        isActive = false;
        target = null;
        
        transform.position = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }

void LateUpdate()
{
    if (!isActive)
        return;
        
        if (target)
        {
            if (Input.GetMouseButton(1)) // 마우스 오른쪽 버튼을 누를 때만 회전
            {
                x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

                y = ClampAngle(y, yMinLimit, yMaxLimit);
            }

            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F) angle += 360F;
        if (angle > 360F) angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
 }
