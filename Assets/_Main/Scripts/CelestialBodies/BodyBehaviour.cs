using UnityEditor.PackageManager;
using UnityEngine;

public class BodyBehaviour : MonoBehaviour
{

    [SerializeField] private CelestialBody body;

    void FixedUpdate()
    {
        Gravity();
    }

    void Gravity()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, body.gravityRadius * transform.localScale.x); // TODO: Change localscale

        foreach (Collider hit in hits)
        {
            if (!hit.CompareTag("Player") && !hit.CompareTag("Debris")) continue;
            Rigidbody rb = hit.attachedRigidbody;

            if (rb == null) continue;

            Vector3 direction = transform.position - rb.worldCenterOfMass;
            if (direction.sqrMagnitude > 0f)
            {
                rb.AddForce(direction.normalized * body.gravityForce, ForceMode.Acceleration);
                Debug.Log(direction);
            }

        }
    }

}
