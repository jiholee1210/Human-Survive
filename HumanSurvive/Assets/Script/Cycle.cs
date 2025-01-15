using UnityEngine;
using UnityEngine.Pool;

public class Cycle : MonoBehaviour, IWeapon
{
    public IObjectPool<GameObject> pool { get; set; }
    private Item item;

    public float GetDamage()
    {
        return item.baseDamage * GameManager.Instance.playerData.finalDamage;
    }

    public void Attack()
    {
        
    }

    public void Init(Item mItem)
    {
        item = mItem;
    }
}
