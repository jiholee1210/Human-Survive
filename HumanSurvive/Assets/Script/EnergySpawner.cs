using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class EnergySpawner : MonoBehaviour, ISpawner
{
    private Item item;
    private float speed;

    private void FixedUpdate() {

    }

    public void Init(Item mItem) {
        item = mItem;
        Spawn();
        if(item.itemLevel == 1) {
            speed = 150f;
            StartCoroutine(Rotate());
        }
    }

    private void Spawn() {
        for(int i = 0; i < item.baseCount; i++) {
            GameObject weapon;
            if(i < transform.childCount) {
                weapon = transform.GetChild(i).gameObject;
            } else {
                weapon = ObjectPoolManager.Instance.GetPooledObject(item.prefabId);
                weapon.transform.parent = transform;
            }
            weapon.transform.localPosition = Vector2.zero;
            weapon.transform.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * i / item.baseCount;
            weapon.transform.Rotate(rotVec);

            weapon.transform.Translate(weapon.transform.right * item.range, Space.World);
            weapon.GetComponent<IWeapon>().Init(item);
        }
    }

    private IEnumerator Rotate() {
        while(true) {
            transform.Rotate(Vector3.back * speed * Time.deltaTime);
            yield return null;
        }
    }

}
