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
    [SerializeField] GameObject box;

    private Rigidbody2D rigidbody2D;
    private Animator animator;
    private CapsuleCollider2D capsuleCollider2D;

    private EnemyMovement enemyMovement;
    private EnemyAttack enemyAttack;
    private EnemyHealth enemyHealth;

    private Transform player;
    [SerializeField] public bool isHorde;
    [SerializeField] public bool isDead = false;

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
        Debug.Log(data.id + " enemy");
        SetEnemy(data);
        isHorde = false;
        isDead = false;
        transform.position = randomPos;
        enemyMovement.SetHorde(isHorde);
        rigidbody2D.linearVelocity = Vector2.zero;
    }

    public void InitHorde(Vector2 randomPos, EnemyData data) {
        Debug.Log(data.id + " enemyHorde");
        SetEnemy(data);
        isHorde = true;
        isDead = false;
        transform.position = randomPos;
        enemyMovement.SetHorde(isHorde);
        enemyMovement.SetSpeed(4);
        rigidbody2D.linearVelocity = Vector2.zero;
    }

    private void SetEnemy(EnemyData data) {
        animator.runtimeAnimatorController = animCon[data.id];
        enemyMovement.SetPlayer(player);
        enemyMovement.SetSpeed(data.speed);
        enemyAttack.SetDamage(data.damage);
        enemyHealth.SetHealth(data.health);
        capsuleCollider2D.size = data.colSize;
        transform.localScale = data.scale;
    }

    private void OnEnable() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyAttack = GetComponent<EnemyAttack>();
        enemyHealth = GetComponent<EnemyHealth>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        player = GameManager.Instance.player.transform;
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

    private void SpawnBox(Vector3 lastPos) {
        Instantiate(box, lastPos, Quaternion.identity);
    }

    public void Die() {
        if(isDead) return;
        isDead = true;
        Vector3 lastPos = transform.position;
        SpawnBox(lastPos);
        pool.Release(gameObject);
    }
}
