using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyData[] enemyData;
    
    private float spawnSpeed;
    private Transform[] spawnPoint;

    private void Awake() {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Start()
    {
        StartCoroutine(GenTimer());
    }

    private void Update() {
        
    }

    private IEnumerator GenTimer() {
        while (true) {
            EnemySpawn();
            yield return new WaitForSeconds(spawnSpeed);
        }
    }

    private void EnemySpawn() {
        // 시간 별 몬스터 스폰
        // 초 단위 변경부터
        int spawn = Mathf.FloorToInt(GameManager.Instance.time / 10) < 4 ? Mathf.FloorToInt(GameManager.Instance.time / 10) + 1 : 4;
        EnemyData data = enemyData[Random.Range(0, spawn)];
        spawnSpeed = data.spawnTime;
        
        GameObject enemy = ObjectPoolManager.Instance.GetPooledObject(0);
        Debug.Log("적 생성됨  : " + enemy + " " + data.id);
        if(enemy != null) {
            Vector2 randomPos = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
            enemy.GetComponent<EnemyManager>().Init(randomPos, data);
        }
    }
}

[System.Serializable]
public class EnemyData {
    public int id;
    public float spawnTime;
    public float health;
    public float damage;
    public float speed;
    public Vector2 colSize;
}
