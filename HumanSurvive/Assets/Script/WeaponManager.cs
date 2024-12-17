using System.Collections;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private Item[] item;

    private Scanner scanner;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake() {
        scanner = GetComponent<Scanner>();
    }

    void Start()
    {
         for (int i = 0; i < item.Length; i++) {
            if(item[i].coolDown != -1) {
                Debug.Log("쿨다운 무기 소환");
                StartCoroutine(SpawnCooldownWeapon(i));
            }
            else {
                SpawnWeapon(i);
            }
         }
    } 

    private IEnumerator SpawnCooldownWeapon(int index) {
        while(true) {
            SpawnWeapon(index);
            yield return new WaitForSeconds(item[index].coolDown);
        }
    }

    private void SpawnWeapon(int index) {
        for(int i = 0; i < item[index].baseCount; i++) {
            if(item[index].isGuided && scanner.nearTarget == null) return; 
            
            GameObject weapon = ObjectPoolManager.Instance.GetPooledObject(item[index].prefabId);
            weapon.transform.parent = transform;
            weapon.transform.position = transform.position;
            weapon.GetComponent<IWeapon>().Init(item[index]);
        }
    }
}
