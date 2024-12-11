using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform player;
    [SerializeField] private float stoppingDistance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
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
            float distance = Vector3.Distance(player.position, transform.position);

            if (distance > stoppingDistance) {
                Vector3 direction = (player.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;
            }
        } else {
            Debug.Log("플레이어를 찾을 수 없습니다.");
        }
    }
}
