using UnityEngine;

public class ObjectRotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5f;
    
    private Vector3 lastMousePosition;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            lastMousePosition = Input.mousePosition;

        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            //float rotationX = delta.y * rotationSpeed;
            float rotationY = -delta.x * rotationSpeed;
            
            transform.Rotate(Vector3.up, rotationY, Space.World);
            //transform.Rotate(Vector3.right, rotationX, Space.World);

            lastMousePosition = Input.mousePosition;
        }
    }
}
