using System.Collections;
using UnityEngine;

public class ShurikenSpawner : MonoBehaviour, ISpawner
{
    private Item item;

    private Scanner scanner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scanner = GetComponent<Scanner>();
    }

    public void Init(Item mItem)
    {
        item = mItem;
        if (item.itemLevel == 1) {
            StartCoroutine(SpawnCoolDown());
        }
    }

    private IEnumerator SpawnCoolDown() {
        while (true) {
            yield return StartCoroutine(Spawn());
            yield return new WaitForSeconds(item.coolDown);
        }
    }
    
    private IEnumerator Spawn() {
        for(int i = 0; i < item.baseCount; i++) {
            if(item.isGuided && scanner.nearTarget == null) break; 
            
            GameObject weapon = ObjectPoolManager.Instance.GetPooledObject(item.prefabId);
            weapon.transform.parent = transform;
            weapon.transform.position = transform.position;
            weapon.GetComponent<IWeapon>().Init(item);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
