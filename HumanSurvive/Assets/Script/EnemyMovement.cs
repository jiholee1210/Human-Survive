using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float speed = 0;

    private Rigidbody2D rigidbody2D;
    private Transform player;
    private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameManager.Instance.player.transform;
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() {
        FollowPlayer();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void FollowPlayer() {
        if (player != null) {
            Vector2 direction = (player.position - transform.position).normalized;
            Vector2 nextVec = direction * speed * Time.deltaTime;
            spriteRenderer.flipX = nextVec.x > 0;
            rigidbody2D.MovePosition(rigidbody2D.position + nextVec);
            rigidbody2D.linearVelocity = Vector2.zero;
            
        } else {
            Debug.Log("플레이어를 찾을 수 없습니다.");
        }
    }

    /*private void OnEnable() {
        player = GameManager.Instance.player.transform;
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }*/

    public void SetSpeed(float spd) {
        speed = spd;
    }
}
