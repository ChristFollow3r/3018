using UnityEngine;

public class DebrisBehaviour : MonoBehaviour
{
    private Rigidbody rb;
    Vector3 randomDirection;
    float randomSpeed;
    void Awake()
    {
        rb = GetComponent<Rigidbody>(); 
    }
    void Start()
    {
        randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        randomSpeed = randomSpeed = Random.Range(1f, 10f);
    }

    void FixedUpdate()
    {
        DebrisMovement();
    }

    void DebrisMovement()
    {

        rb.linearVelocity = randomDirection * randomSpeed;
    }
}
