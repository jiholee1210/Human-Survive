using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyManager : MonoBehaviour
{
    public IObjectPool<GameObject> pool {get; set;}

    [SerializeField] RuntimeAnimatorController[] animCon;
    [SerializeField] LayerMask bulletLayer;

    private Rigidbody2D rigidbody2D;
    private Animator animator;
    private CapsuleCollider2D capsuleCollider2D;

    private EnemyMovement enemyMovement;
    private EnemyAttack enemyAttack;
    private EnemyHealth enemyHealth;

    private GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyAttack = GetComponent<EnemyAttack>();
        enemyHealth = GetComponent<EnemyHealth>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Init(Vector2 randomPos, EnemyData data)
    {
        Debug.Log(data.id);
        SetEnemy(data);
        transform.position = randomPos;
        rigidbody2D.linearVelocity = Vector2.zero;
    }

    private void SetEnemy(EnemyData data) {
        animator.runtimeAnimatorController = animCon[data.id];
        enemyMovement.SetSpeed(data.speed);
        enemyAttack.SetDamage(data.damage);
        enemyHealth.SetHealth(data.health);
        capsuleCollider2D.size = data.colSize;
    }

    private void OnEnable() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyAttack = GetComponent<EnemyAttack>();
        enemyHealth = GetComponent<EnemyHealth>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        player = GameManager.Instance.player;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (((1 << other.gameObject.layer) & bulletLayer) != 0) {
            IWeapon weapon = other.GetComponent<IWeapon>();
            enemyHealth.OnHit(weapon.GetDamage());
            if(enemyHealth.GetHealth() <= 0) {
                Die();
                GameManager.Instance.SetKillCount(1);
                GameManager.Instance.SetPlayerExp(2);
            }
        }
    }

    private void Die() {
        pool.Release(gameObject);
    }
}
