using UnityEngine;

public class LizardSpear : MonoBehaviour, IBoss
{
    [SerializeField] private Transform target;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask boundary;
    private float spearDamage;

    private Rigidbody2D rigidbody2D;

    private Vector2 dir;
    private Quaternion rotation;

    void FixedUpdate() {
        Attack();
    }

    public void Init() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        target = GameManager.Instance.player.transform;
        dir = (target.position - transform.position).normalized;
        rotation =  Quaternion.FromToRotation(Vector3.up, dir);
        spearDamage = 20f;
    }

    private void Attack() {
        if (target != null) {
            rigidbody2D.MovePositionAndRotation(rigidbody2D.position + (dir * 10f * Time.deltaTime), rotation);
        }
    }

    public float GetDamage() {
        return spearDamage;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (((1 << other.gameObject.layer) & playerLayer) != 0) {
            other.GetComponent<PlayerHealth>().OnHit(spearDamage);
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (((1 << other.gameObject.layer) & boundary) != 0) {
            Destroy(gameObject);
        }
    }
}
