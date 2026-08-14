using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float trustForce = 5f;

    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementDirection();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //TODO тут можно будет добавить логику для обработки столкновений с препятствиями, например, уменьшение здоровья игрока или проигрыш.
        Destroy(gameObject);
    }

    Vector2 CalculateMouseDirection()
    {
        return (CalculateMousePosition() - transform.position).normalized;;
    }

    Vector3 CalculateMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Mouse.current.position.value);;
    }

    void MovementDirection()
    {
        if(Mouse.current.leftButton.isPressed)
        {
            transform.up = CalculateMouseDirection();
            rb.AddForce(transform.up * trustForce);
        }
    }
}
