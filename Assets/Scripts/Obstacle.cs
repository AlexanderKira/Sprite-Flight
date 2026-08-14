using System.Numerics;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float minSize = 0.5f;
    public float maxSize = 2.0f;

    public float minSpeed = 100f;
    public float maxSpeed = 300f;

    public float maxSpinSpeed = 10f;

    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float randomSize = Random.Range(minSize, maxSize);
        float randomSpeed = Random.Range(minSpeed, maxSpeed) / randomSize;
        float randomTorque = Random.Range(-maxSpinSpeed, maxSpinSpeed);
        UnityEngine.Vector2 randomDirection = Random.insideUnitCircle;
        transform.localScale = new UnityEngine.Vector3(randomSize, randomSize, 1);
        
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(randomDirection * randomSpeed);
        rb.AddTorque(randomTorque);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
