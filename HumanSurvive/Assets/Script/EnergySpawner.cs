using UnityEngine;
using UnityEngine.Pool;

public class EnergySpawner : MonoBehaviour, ISpawner
{
    private Item item;
    private float speed;

    private void FixedUpdate() {
        Attack();
    }

    public float GetDamage()
    {
        return item.baseDamage;
    }

    public void Attack()
    {
        transform.Rotate(Vector3.forward * speed * Time.deltaTime);
    }

    public void Init(Item mItem)
    {
        item = mItem;
        speed = 100f;
    }

    public void LevelUp(Item mItem) {

    }

}
