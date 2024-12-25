using UnityEngine;
using UnityEngine.Pool;

public class Swing : MonoBehaviour, IWeapon
{
    public IObjectPool<GameObject> pool { get; set; }
    private Item item;

    public float GetDamage() {
        return item.baseDamage;
    }

    public void Attack() {

    }

    public void Init(Item mItem) {
        item = mItem;
    }
}
