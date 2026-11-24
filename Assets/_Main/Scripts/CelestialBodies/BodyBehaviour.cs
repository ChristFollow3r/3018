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
        float gravityRange = body.gravityRadius * Mathf.Abs(transform.lossyScale.x); // Make the range change based on the object scale
        Collider[] hits = Physics.OverlapSphere(transform.position, gravityRange);

        foreach (Collider hit in hits)
        {
            Rigidbody rb = hit.attachedRigidbody;

            if (rb == null) continue;
            if (!rb.CompareTag("Player") && !rb.CompareTag("Debris")) continue;

            Vector3 direction = transform.position - rb.worldCenterOfMass;
            float distance = direction.magnitude;

            if (distance <= 0f || distance >= gravityRange) continue; // Sop applying gravity if its out of the range or if its already touching the body

            float falloff = 1f - distance / gravityRange;
            float currentForce = body.gravityForce * falloff;
            rb.AddForce(direction.normalized * currentForce, ForceMode.Acceleration);
            Debug.Log(direction);

        }
    }

}
