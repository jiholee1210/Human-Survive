using UnityEngine;
using UnityEngine.Pool;

public class Shuriken : MonoBehaviour, IWeapon
{
    public IObjectPool<GameObject> pool {get; set;}
    
    [SerializeField] private Transform target;
    [SerializeField] private LayerMask boundary;
    [SerializeField] private LayerMask enemyLayer;

    private Vector2 dir;
    private Quaternion rotation;
    private Item item;
    private bool isHit = false;

    private Rigidbody2D rigidbody2D;

    private void Start() {
        dir = (target.position - transform.position).normalized;
        rotation =  Quaternion.FromToRotation(Vector3.right, dir);
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        Attack();
    }

    public void Attack() {
        rigidbody2D.MovePositionAndRotation(rigidbody2D.position + (dir * item.speed * Time.deltaTime), rotation);
    }

    public float GetDamage() {
        return item.baseDamage * item.finalDamage;
    }

    public void Init(Item mItem) {
        item = mItem;
        isHit = false;
        target =  GetComponentInParent<Scanner>().nearTarget;
        Debug.Log(target + "타겟 찾음");
        dir = (target.position - transform.position).normalized;
        rotation =  Quaternion.FromToRotation(Vector3.right, dir);
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (((1 << other.gameObject.layer) & boundary) != 0) {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(((1 << other.gameObject.layer) & enemyLayer) != 0) {
            Die();
        }
    }

    private void Die() {
        if(isHit) {
            return;
        }
        isHit = true;
        pool.Release(gameObject);
    }
}
