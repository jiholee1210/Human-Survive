using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 0.3f;

    float inputValueX;
    float inputValueY;
    float currentSpeedX;
    float currentSpeedY;

    private Rigidbody2D rigidbody2D;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        currentSpeedX = inputValueX * speed;
        currentSpeedY = inputValueY * speed;

        rigidbody2D.linearVelocityX = currentSpeedX;
        rigidbody2D.linearVelocityY = currentSpeedY;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove(InputValue value) {
        inputValueX = value.Get<Vector2>().x;
        inputValueY = value.Get<Vector2>().y;
    }
}
