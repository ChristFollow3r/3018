using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed = 7f;
    [SerializeField] private Camera playerCamera;
    private Rigidbody rb;
    private InputSystem_Actions input;
    
    [SerializeField] BodyBehaviour[] bodies;
    private GameObject lastBody;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        foreach (var body in bodies)
        {
            body.onPlayerIntercepted += HandlePlayerIntercepted;
        }
    }

    void Start()
    {
        rb.useGravity = false;
        input = new InputSystem_Actions();
        input.Enable();
    }


    void FixedUpdate()
    {
        Locomotion();
    }

    void Locomotion()
    {
        Vector3 up = lastBody != null ? (rb.worldCenterOfMass - lastBody.transform.position).normalized : Vector3.up;
        Vector3 cameraForward = Vector3.ProjectOnPlane(playerCamera.transform.forward, up).normalized; // TODO: Understand this shit
        Vector3 cameraRight = Vector3.ProjectOnPlane(playerCamera.transform.right, up).normalized; // TODO: Understand this shit
        Vector2 movement = input.Player.Move.ReadValue<Vector2>();

        Vector3 direction = cameraRight * movement.x + cameraForward * movement.y;

        if (direction.sqrMagnitude > 1f) direction.Normalize(); // Diference between sqrMagnitude and Magntiude might be that one is fucked up and the other isnt

        Vector3 radialVelocity = Vector3.Project(rb.linearVelocity, up); // TODO: Understand this shit
        Vector3 velocity = direction * speed;

        rb.linearVelocity = velocity + radialVelocity; // TODO: Understand this shit

        if (lastBody != null)
        {
            Vector3 forward = direction.sqrMagnitude > 0.001f ? direction : transform.forward;
            forward = Vector3.ProjectOnPlane(forward, up);

            if (forward.sqrMagnitude > 0.001f)
            {
                Quaternion target = Quaternion.LookRotation(forward, up);
                rb.MoveRotation(Quaternion.Slerp(rb.rotation, target, 15f * Time.fixedDeltaTime));
            }
        }
    }

    void HandlePlayerIntercepted(GameObject body)
    {
        if (lastBody != null && lastBody.name == body.name) return;
        lastBody = body;
    }

}
