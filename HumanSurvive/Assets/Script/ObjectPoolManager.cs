using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance { get; private set; }
    
    [SerializeField] GameObject[] prefabs;

    private int size = 10;
    private Dictionary<int, ObjectPool<GameObject>> poolDic = new Dictionary<int, ObjectPool<GameObject>>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake() {
        Instance = this;
        Init();
    }

    private void Init() {
        for (int i = 0; i < prefabs.Length; i++) {
            int currentIndex = i;
            ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
                () => CreatePooledItem(currentIndex), OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject
            );
            poolDic.Add(currentIndex, pool);
            Debug.Log(currentIndex + " 풀 생성");
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
                default:
                    poolGo.GetComponent<IWeapon>().pool = poolDic[id];
                    Debug.Log(id + "번 무기 풀 연결 완료");
                    break;
            }
            return poolGo;
        }
        return null;
    }

    public void OnTakeFromPool(GameObject poolGo) {
        if(poolGo == null) {
            return;
        }
        poolGo.SetActive(true);
    }

    public void OnReturnedToPool(GameObject poolGo) {
        if (poolGo == null) {
            return;
        }
        poolGo.SetActive(false);
    }

    public void OnDestroyPoolObject(GameObject poolGo) {
        if (poolGo == null) {
            return;
        }
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
