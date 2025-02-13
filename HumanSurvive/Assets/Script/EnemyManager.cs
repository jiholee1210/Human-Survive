using System;
using System.Collections;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyManager : MonoBehaviour
{
    public IObjectPool<GameObject> pool {get; set;}

    [SerializeField] RuntimeAnimatorController[] animCon;
    [SerializeField] RuntimeAnimatorController[] bossAnimCon;
    [SerializeField] LayerMask bulletLayer;
    [SerializeField] GameObject box;
    [SerializeField] GameObject coinBag;
    [SerializeField] GameObject meat;
    [SerializeField] GameObject damageText;

    private Rigidbody2D rigidbody2D;
    private Animator animator;
    private CapsuleCollider2D capsuleCollider2D;

    private EnemyMovement enemyMovement;
    private EnemyAttack enemyAttack;
    private EnemyHealth enemyHealth;
    private IMonster[] ability;
    

    private Transform player;
    private Vector3 lastPos;
    [SerializeField] public bool isHorde;
    [SerializeField] public bool isDead = false;
    [SerializeField] public bool isBoss;

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
        isDead = false;
        transform.position = randomPos;
        enemyMovement.SetHorde(isHorde);
        rigidbody2D.linearVelocity = Vector2.zero;
    }

    public void InitBoss(Vector2 randomPos, EnemyData data) {
        isBoss = true;
        Debug.Log(data.id + " enemyBoss");
        SetEnemy(data);
        isDead = false;
        transform.position = randomPos;
        enemyMovement.SetHorde(isHorde);
        rigidbody2D.linearVelocity = Vector2.zero;
        ability = GetComponentsInChildren<IMonster>();
        foreach (var ability in ability) {
            Debug.Log("Found ability : " + ability.GetType().Name);
        }
        //ability[0].Ability();
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
        animator.runtimeAnimatorController = isBoss ? bossAnimCon[data.id] : animCon[data.id];
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
            lastPos = transform.position;
            ShowDamage(lastPos, weapon.GetDamage());
            if(enemyHealth.GetHealth() <= 0) {
                Die();
                GameManager.Instance.SetKillCount(1);
                GameManager.Instance.SetPlayerExp(2);
            }
        }
    }

    private void ShowDamage(Vector3 pos, float damage) {
        pos.z -= 1;
        GameObject text = Instantiate(damageText, pos, Quaternion.identity);
        text.GetComponent<TMP_Text>().text = Mathf.FloorToInt(damage).ToString();
    }

    private void SpawnBox(Vector3 lastPos) {
        Instantiate(box, lastPos, Quaternion.identity);
    }

    private void SpawnBag(Vector3 lastPos) {
        Instantiate(coinBag, lastPos, Quaternion.identity);
    }

    private void SpawnMeat(Vector3 lastPos) {
        Instantiate(meat, lastPos, Quaternion.identity);
    }

    public void Die() {
        if(isDead) return;
        isDead = true;
        isHorde = false;

        if(isBoss) {
            SpawnBox(lastPos);
            isBoss = false;
        }
        else {
            int random = UnityEngine.Random.Range(0, 100);
            
            if(random >= 90) SpawnBag(lastPos);
            else if(random >= 80) SpawnMeat(lastPos);
        }
        pool.Release(gameObject);
    }
}
