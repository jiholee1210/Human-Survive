using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] float currentHp = 100f;
    [SerializeField] float defaultHp = 0;
    [SerializeField] LayerMask enemyLayer;

    private PlayerManager playerManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
    }

    private void FixedUpdate() {
        if (currentHp < 0) {
            Die();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
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
