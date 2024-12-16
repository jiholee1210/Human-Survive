using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 0.3f;

    float inputValueX;
    float inputValueY;
    float input;
    float currentSpeedX;
    float currentSpeedY;

    public Vector2 inputVec;

    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        currentSpeedX = inputValueX * speed;
        currentSpeedY = inputValueY * speed;
        inputVec = new Vector2(inputValueX, inputValueY);

        input = (Mathf.Abs(inputValueX) + Mathf.Abs(inputValueY) >= 1) ? 1 : 0;

        animator.SetFloat("Input", Mathf.Abs(input));

        rigidbody2D.linearVelocityX = currentSpeedX;
        rigidbody2D.linearVelocityY = currentSpeedY;

        if(inputValueX != 0) {
            spriteRenderer.flipX = inputValueX < 0;
        }
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
