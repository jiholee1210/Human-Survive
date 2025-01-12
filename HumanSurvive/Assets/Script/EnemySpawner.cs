using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyData[] enemyData;
    
    private float spawnSpeed;
    private Transform[] spawnPoint;
    private int bossSpawnCheck;
    private int hordeSpawnCheck;

    private void Awake() {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Start()
    {
        bossSpawnCheck = 1;
        hordeSpawnCheck = 1;
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
        int bossSpawn = Mathf.FloorToInt(GameManager.Instance.time / 30) < 4 ? Mathf.FloorToInt(GameManager.Instance.time / 30) : 3;
        int hordeSpawn = Mathf.FloorToInt(GameManager.Instance.time / 10);
        Spawn(enemyData[0]);
        if(bossSpawn == bossSpawnCheck) {
            Spawn(enemyData[bossSpawn]);
            bossSpawnCheck++;
        }
        if(hordeSpawn == hordeSpawnCheck) {
            SpawnHorde(enemyData[0], 10);
            hordeSpawnCheck++;
        }
    }

    private void Spawn(EnemyData enemyData) {
        if(enemyData.spawnTime != -1) {
            spawnSpeed = enemyData.spawnTime;
        }
        GameObject enemy = ObjectPoolManager.Instance.GetPooledObject(0);
        if(enemy != null) {
            Vector2 randomPos = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
            enemy.GetComponent<EnemyManager>().Init(randomPos, enemyData);
        }
    }

    private void SpawnHorde(EnemyData enemyData, int count) {
        Vector2 randomPos = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        
        for(int i = 0; i < count; i++) {
            float offsetX = Random.Range(-1f, 1f);
            float offsetY = Random.Range(-1f, 1f);
            Vector2 spawnPos = new Vector2(randomPos.x + offsetX, randomPos.y + offsetY);
            GameObject enemy = ObjectPoolManager.Instance.GetPooledObject(0);

            if(enemy != null) {
                enemy.GetComponent<EnemyManager>().InitHorde(spawnPos, enemyData);
            }
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
    public Vector2 scale;
}
