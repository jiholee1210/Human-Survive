using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform player;
    [SerializeField] private float stoppingDistance;

    private Rigidbody2D rigidbody2D;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        rigidbody2D = GetComponent<Rigidbody2D>();
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
            float distance = Vector2.Distance(player.position, transform.position);

            if (distance > stoppingDistance) {
                Vector2 direction = (player.position - transform.position).normalized;
                Vector2 nextVec = direction * speed * Time.deltaTime;
                rigidbody2D.MovePosition(rigidbody2D.position + nextVec);
                rigidbody2D.linearVelocity = Vector2.zero;
            }
        } else {
            Debug.Log("플레이어를 찾을 수 없습니다.");
        }
    }
}
