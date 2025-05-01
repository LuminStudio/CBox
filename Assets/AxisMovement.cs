using UnityEngine;

public class AxisMovement : MonoBehaviour
{
    public Transform xAxis;
    public Transform yAxis;
    public Transform zAxis;
    public float sensitivity = 1.0f; // Чувствительность перемещения

    private Transform selectedAxis = null;
    private Vector3 lastMousePosition;

    void Update()
    {
        HandleMouseInput();
        HandleAxisMovement();
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == xAxis)
                {
                    selectedAxis = xAxis;
                }
                else if (hit.transform == yAxis)
                {
                    selectedAxis = yAxis;
                }
                else if (hit.transform == zAxis)
                {
                    selectedAxis = zAxis;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            selectedAxis = null;
        }
    }

    void HandleAxisMovement()
    {
        if (selectedAxis != null)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 deltaMousePosition = currentMousePosition - lastMousePosition;

            if (selectedAxis == xAxis)
            {
                transform.Translate(Vector3.right * deltaMousePosition.x * sensitivity * Time.deltaTime, Space.World);
            }
            else if (selectedAxis == yAxis)
            {
                transform.Translate(Vector3.up * deltaMousePosition.y * sensitivity * Time.deltaTime, Space.World);
            }
            else if (selectedAxis == zAxis)
            {
                transform.Translate(Vector3.forward * deltaMousePosition.x * sensitivity * Time.deltaTime, Space.World);
            }

            lastMousePosition = currentMousePosition;
        }
        else
        {
            lastMousePosition = Input.mousePosition;
        }
    }
}