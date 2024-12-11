using UnityEngine;

public interface IDamageable
{
    void OnHit(float damage);
    void RestoreHp(float heal);
    void Die();
}
