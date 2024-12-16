using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance { get; private set; }
    
    [SerializeField] GameObject[] enemy;

    private int size = 10;
    private Dictionary<string, IObjectPool<GameObject>> poolDic = new Dictionary<string, IObjectPool<GameObject>>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake() {
        Instance = this;
        Init();
    }

    private void Init() {
        for (int i = 0; i < enemy.Length; i++) {
            string prefabName = enemy[i].name;
            IObjectPool<GameObject> pool = new ObjectPool<GameObject>(
                () => CreatePooledItem(prefabName), OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, true, size, size
            );
            poolDic.Add(prefabName, pool);

            for (int j = 0; j < size; j++) {
                GameObject pooledObject = CreatePooledItem(prefabName);
                pool.Release(pooledObject);
            }
        }
    }

    public GameObject CreatePooledItem(string name) {
        GameObject prefab = System.Array.Find(enemy, obj => obj.name == name);

        if (prefab != null) {
            GameObject poolGo = Instantiate(prefab);
            poolGo.GetComponent<EnemyManager>().pool = poolDic[name];
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

    public GameObject GetPooledObject(string name) {
        if(poolDic.ContainsKey(name)) {
            IObjectPool<GameObject> pool = poolDic[name];
            return pool.Get();
        }
        return null;
    }
}
