using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class FireBall : MonoBehaviour, IWeapon
{
    public IObjectPool<GameObject> pool {get; set;}
    
    [SerializeField] private Transform target;
    [SerializeField] private LayerMask boundary;

    private Vector2 dir;
    private Quaternion rotation;
    private float speed = 8f;
    private Item item;

    private Rigidbody2D rigidbody2D;
    private Animator animator;

    private void Start() {
        dir = (target.position - transform.position).normalized;
        rotation =  Quaternion.FromToRotation(Vector3.right, dir);
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        Attack();
    }

    public void Attack() {
        rigidbody2D.MovePositionAndRotation(rigidbody2D.position + (dir * speed * Time.deltaTime), rotation);
    }

    public float GetDamage() {
        return item.baseDamage;
    }

    public void Init(Item mItem) {
        item = mItem;
        target =  GetComponentInParent<Scanner>().nearTarget;
        dir = (target.position - transform.position).normalized;
        rotation =  Quaternion.FromToRotation(Vector3.right, dir);
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = mItem.animCon;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (((1 << other.gameObject.layer) & boundary) != 0) {
            Die();
        }
    }

    private void Die() {
        pool.Release(gameObject);
    }
}
