using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance { get; private set; }
    
    [SerializeField] GameObject[] prefabs;

    private int size = 10;
    private Dictionary<int, IObjectPool<GameObject>> poolDic = new Dictionary<int, IObjectPool<GameObject>>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake() {
        Instance = this;
        Init();
    }

    private void Init() {
        for (int i = 0; i < prefabs.Length; i++) {
            int currentIndex = i;
            IObjectPool<GameObject> pool = new ObjectPool<GameObject>(
                () => CreatePooledItem(currentIndex), OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, true, size, size
            );
            poolDic.Add(currentIndex, pool);

            /*for (int j = 0; j < size; j++) {
                GameObject pooledObject = CreatePooledItem(currentIndex);
                pool.Release(pooledObject);
            }*/
        }
    }

    public GameObject CreatePooledItem(int id) {
        Debug.Log("CreatePoolItem : " + id);
        GameObject prefab = prefabs[id];

        if (prefab != null) {
            GameObject poolGo = Instantiate(prefab);
            switch (id) {
                case 0:
                    poolGo.GetComponent<EnemyManager>().pool = poolDic[id];
                    break;
                case 1:
                    poolGo.GetComponent<IWeapon>().pool = poolDic[id];
                    break;
            }
            return poolGo;
        }
        return null;
    }

    public void OnTakeFromPool(GameObject poolGo) {
        poolGo.SetActive(true);
    }

    public void OnReturnedToPool(GameObject poolGo) {
        poolGo.SetActive(false);
    }

    public void OnDestroyPoolObject(GameObject poolGo) {
        Destroy(poolGo);
    }

    public GameObject GetPooledObject(int id) {
        if(poolDic.ContainsKey(id)) {
            IObjectPool<GameObject> pool = poolDic[id];
            return pool.Get();
        }
        return null;
    }
}
