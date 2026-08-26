using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Label scoreText;
    public UIDocument uIDocument;
    private float elapsedTime = 0f;

    private float score = 0f;

    public float scoreMultiplier = 10f;

    public float trustForce = 5f;

    public float maxSpeed = 10f;

    public GameObject explosionEffect;

    Rigidbody2D rb;

    public GameObject boosterFlame;

    private Button restartButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scoreText = uIDocument.rootVisualElement.Q<Label>("ScoreLabel");
        restartButton = uIDocument.rootVisualElement.Q<Button>("RestartButton");
        restartButton.clicked += ReloadScene;
        restartButton.style.display = DisplayStyle.None;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();

        MovementDirection();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        //TODO тут можно будет добавить логику для обработки столкновений с препятствиями, например, уменьшение здоровья игрока или проигрыш.
        Destroy(gameObject);
        restartButton.style.display = DisplayStyle.Flex;
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
            if(rb.linearVelocity.magnitude > maxSpeed)
            {
                //TODO тут можно добавить вызов анимации для ограничения скорости игрока, например, воспроизведение анимации
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }
        }

        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            boosterFlame.SetActive(true);
        }
        else if(Mouse.current.leftButton.wasReleasedThisFrame)
        {
            boosterFlame.SetActive(false);
        }
    }

    void UpdateScore()
    {
        elapsedTime += Time.deltaTime;
        //если 3.7=4 начисление баллов произойдет еще до того, как прошла бы полная секунда.
        score = Mathf.FloorToInt(elapsedTime * scoreMultiplier);

        scoreText.text = "Score:" + score; 
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
