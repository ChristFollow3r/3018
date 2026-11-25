using UnityEngine;

public class DebrisBehaviour : MonoBehaviour
{
    private Rigidbody rb;
    float minSpeed;
    float maxSpeed;
    void Awake()
    {
        rb = GetComponent<Rigidbody>(); 
    }
    void Start()
    {
        rb.useGravity = false;
        rb.linearVelocity = Random.onUnitSphere * Random.Range(minSpeed, maxSpeed);
    }


}

