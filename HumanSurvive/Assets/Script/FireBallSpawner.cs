using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class FireBallSpawner : MonoBehaviour, ISpawner
{
    private Item item;

    private Scanner scanner;

    private void Start() {
        scanner = GetComponent<Scanner>();
    }

    public void Init(Item mItem) {
        item = mItem;
        if (item.itemLevel == 1) {
            StartCoroutine(SpawnCoolDown());
        }
    }

    private IEnumerator SpawnCoolDown() {
        while (true) {
            Spawn();
            yield return new WaitForSeconds(item.coolDown);
        }
    }

    private void Spawn() {
        for(int i = 0; i < item.baseCount; i++) {
            if(item.isGuided && scanner.nearTarget == null) return; 
            
            GameObject weapon = ObjectPoolManager.Instance.GetPooledObject(item.prefabId);
            weapon.transform.parent = transform;
            weapon.transform.position = transform.position;
            weapon.GetComponent<IWeapon>().Init(item);
        }
    }
}
