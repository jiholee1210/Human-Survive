using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] float maxHp;
    [SerializeField] float currentHp;
    [SerializeField] float defaultHp;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] LayerMask bossAttackLayer;

    private PlayerManager playerManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        defaultHp = 100f * GameManager.Instance.playerData.hp;
        maxHp = defaultHp;
        currentHp = defaultHp;
        playerManager = GetComponent<PlayerManager>();
    }

    private void FixedUpdate() {
        if (currentHp <= 0) {
            Die();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMaxHp() {
        maxHp = defaultHp * GameManager.Instance.playerData.hp;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (((1 << other.gameObject.layer) & enemyLayer) != 0) {
            EnemyAttack enemyAttack = other.gameObject.GetComponent<EnemyAttack>();
            OnHit(enemyAttack.GetDamage());
        }
    }

    public void OnHit(float damage)
    {
        currentHp -= damage;
        Debug.Log("" + currentHp);
    }

    public void RestoreHp(float heal)
    {
        currentHp += heal;
    }

    public void Die()
    {
        playerManager.Die();
    }
}
