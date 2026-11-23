using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed = 7f;
    [SerializeField] private Camera playerCamera;
    private Rigidbody rb;
    private InputSystem_Actions input;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        input = new InputSystem_Actions();
        input.Enable();
    }

    void FixedUpdate()
    {
        Locomotion();
    }

    void Locomotion()
    {
        Vector2 movement = input.Player.Move.ReadValue<Vector2>();
        Vector3 cameraForward = playerCamera.transform.forward;
        Vector3 cameraRight = playerCamera.transform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 direction = cameraRight * movement.x + cameraForward * movement.y;
        if (direction.sqrMagnitude > 1f) direction.Normalize();

        Vector3 velocity = direction * speed;
        velocity.y = rb.linearVelocity.y;
        rb.linearVelocity = velocity;

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion target = Quaternion.LookRotation(direction);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, target, 15f * Time.fixedDeltaTime));
        }

    }
}
