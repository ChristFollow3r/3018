using UnityEditor.PackageManager;
using UnityEngine;

public class BodyBehaviour : MonoBehaviour
{

    [SerializeField] private CelestialBody body;

    // Update is called once per frame
    void LateUpdate()
    {

    }

    void Gravity()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, body.gravityRadius);

        foreach (Collider hit in hits)
        {
            if (!hit.CompareTag("player") || !hit.CompareTag("debris")) return;
        }
    }
}
