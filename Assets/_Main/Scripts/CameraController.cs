using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform lookAt;
    [SerializeField] private Transform player;

    [Header("Mouse Settings")]
    [SerializeField] private float mouseSensitivity = .1f;
    [SerializeField] private float maxPitch = 60f;
    [SerializeField] private float minPitch = -20f;
    [SerializeField] private float distance = 10f;

    private float yaw;
    private float pitch = 15f;
    private Vector3 velocity;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Locks the cursor to the center of the screen
        Cursor.visible = false;

        yaw = transform.eulerAngles.y;
    }

    void LateUpdate()
    {
        CameraMovement();
    }

    void CameraMovement()
    {
        Vector2 delta = Mouse.current.delta.ReadValue();

        yaw += delta.x * mouseSensitivity;
        pitch -= delta.y * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 targetPosition = lookAt.position + rotation * new Vector3(0, 0, -distance);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 0.1f);
        transform.rotation = rotation;

    }
}
