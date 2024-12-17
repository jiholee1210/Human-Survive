using UnityEngine;
using UnityEngine.Pool;

public interface IWeapon
{
    float GetDamage();
    void Attack();
    void Init(Item item);
    IObjectPool<GameObject> pool { get; set; }
}
