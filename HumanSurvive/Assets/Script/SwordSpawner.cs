using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class SwordSpawner : MonoBehaviour, ISpawner
{

    public IObjectPool<GameObject> pool { get; set; }

    private float dir;
    private Item item;

    private void Start() {
        dir = -1f;
    }

    public void Init(Item mItem) {
        item = mItem;
        Spawn();
        if(item.itemLevel == 1) {
            transform.rotation = Quaternion.Euler(0, 0, 45f);
            dir = -1f;
            StartCoroutine(Swing());
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

            weapon.transform.Translate(weapon.transform.right * 1.5f, Space.World);
            weapon.GetComponent<IWeapon>().Init(item);
        }
    }

    private IEnumerator RotateSword() {
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = startRotation * Quaternion.Euler(0, 0, 90f * dir);

        float duration = 0.2f;
        float curTime = 0f;

        while(curTime < duration) {
            curTime += Time.deltaTime;
            float t = 1 - Mathf.Pow(1 - (curTime / duration), 2);
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);
            yield return null;
        }
    }

    private IEnumerator Swing() {
        while(true) {
            yield return StartCoroutine(RotateSword());
            yield return new WaitForSeconds(0.3f);
            dir *= -1;
        }
    }
}
