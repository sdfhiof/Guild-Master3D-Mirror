using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public Transform Target;
    public float MouseSensitivity = 10f;

    private float verticalRotation;
    private float horizontalRotation;

    void LateUpdate()
    {
        CameraFollow();
        CameraRotation();
    }

    void CameraFollow()
    {
        transform.position = Target.position;
    }

    void CameraRotation()
    {
        if (Target == null)
        {
            return;
        }        

        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            verticalRotation -= mouseY * MouseSensitivity;
            verticalRotation = Mathf.Clamp(verticalRotation, -70f, 70f);

            horizontalRotation += mouseX * MouseSensitivity;

            transform.rotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);
        }
    }
}