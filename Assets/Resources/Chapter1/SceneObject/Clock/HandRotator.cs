// HandRotator

using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HandRotator : MonoBehaviour
{
    Camera cam;
    bool isDragging;
    public bool IsDragging { get { return isDragging; } }
    float prevAngle;

    void Start()
    {
        cam = Camera.main;
    }

    void OnMouseDown()
    {
        isDragging = true;
        prevAngle = GetMousePivotAngle();
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    void Update()
    {
        if (!isDragging) return;

        float currentAngle = GetMousePivotAngle();
        float delta = currentAngle - prevAngle;
        if (delta < 0f) delta += 360f;

        // z축 회전만 누적
        transform.Rotate(Vector3.forward, delta, Space.Self);
        prevAngle = currentAngle;
    }

    float GetMousePivotAngle()
    {
        var ray = cam.ScreenPointToRay(Input.mousePosition);
        var plane = new Plane(Vector3.forward, Vector3.zero);
        plane.Raycast(ray, out float enter);
        Vector3 mouseWorld = ray.GetPoint(enter);

        Vector3 pivot = transform.position;
        Vector3 dir = mouseWorld - pivot;
        float baseA = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return (90f - baseA + 360f) % 360f;
    }
}
