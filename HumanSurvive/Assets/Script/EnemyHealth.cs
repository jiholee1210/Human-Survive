using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHealth(float hp) {
        health = hp;
    }

    public float GetHealth() {
        return health;
    }

    public void OnHit(float damage)
    {
        health -= damage;
    }

    public void RestoreHp(float heal)
    {
        health += heal;
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
