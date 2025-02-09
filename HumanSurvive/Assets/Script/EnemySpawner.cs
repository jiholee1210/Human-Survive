using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyData[] enemyData;
    [SerializeField] EnemyData[] bossData;
    
    private Transform[] spawnPoint;
    private int hordeSpawnCheck;

    private void Awake() {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Start()
    {
        hordeSpawnCheck = 1;
        GenTimer();
    }

    private void Update() {
        
    }

    private void GenTimer() {
        StartCoroutine(EnemySpawn());
        StartCoroutine(BossSpawn());
    }

    private IEnumerator EnemySpawn() {
        // 시간 별 몬스터 스폰
        // 초 단위 변경부터
        
        // 1 웨이브 : 박쥐 + 박쥐 호드
        for(float timer = 0; timer < 30f; timer += 0.4f) {
            Spawn(enemyData[0]);
            yield return new WaitForSeconds(0.4f);
            // 박쥐 무리
        }

        // 2 웨이브 : 뱀 + 뱀 호드
        for(float timer = 0; timer < 60f; timer += 0.3f) {
            Spawn(enemyData[1]);
            yield return new WaitForSeconds(0.3f);
            // 뱀 무리
        }

        // 3 웨이브 : 리자드
        for(float timer = 0; timer < 60f; timer += 0.7f) {
            Spawn(enemyData[2]);
            yield return new WaitForSeconds(0.7f);
        }

        // 4 웨이브 : 전갈
        for(float timer = 0; timer < 30f; timer += 0.4f) {
            Spawn(enemyData[3]);
            yield return new WaitForSeconds(0.4f);
        }

        // 5 웨이브 : 선인장
        for(float timer = 0; timer < 60f; timer += 0.2f) {
            Spawn(enemyData[4]);
            yield return new WaitForSeconds(0.2f);
        }

        // 6 웨이브 : 박쥐 + 뱀 다수
        for(float timer = 0; timer < 60f; timer += 0.4f) {
            Spawn(enemyData[0]);
            Spawn(enemyData[1]);
            yield return new WaitForSeconds(0.4f);
        }

        // 7 웨이브 : 물정령 + 뱀 호드
        for(float timer = 0; timer < 60f; timer += 0.4f) {
            Spawn(enemyData[5]);
            yield return new WaitForSeconds(0.4f);
            // 뱀 무리
        }

        // 8 웨이브 : 거북맨
        for(float timer = 0; timer < 60f; timer += 0.5f) {
            Spawn(enemyData[6]);
            yield return new WaitForSeconds(0.5f);
        }

        // 9 웨이브 : 탕구리 + 거북맨
        for(float timer = 0; timer < 30f; timer += 0.4f) {
            Spawn(enemyData[6]); // 탕구리
            yield return new WaitForSeconds(0.4f);
        }

        // 10 웨이브 : 선인장 다수
        for(float timer = 0; timer < 30f; timer += 0.3f) {
            Spawn(enemyData[4]);
            yield return new WaitForSeconds(0.3f);
        }

        // 11 웨이브 : 물정령 + 선인장 다수
        for(float timer = 0; timer < 30f; timer += 0.3f) {
            Spawn(enemyData[5]);
            yield return new WaitForSeconds(0.3f);
        }

        // 12 웨이브 : 거북맨 + 물정령 다수
        for(float timer = 0; timer < 30f; timer += 0.3f) {
            Spawn(enemyData[6]);
            yield return new WaitForSeconds(0.3f);
        }

        // 13 웨이브 : 탕구리 + 거북맨 다수
        for(float timer = 0; timer < 30f; timer += 0.3f) {
            Spawn(enemyData[6]); // 탕구리
            yield return new WaitForSeconds(0.3f);
        }

        // 14 웨이브 : 탕구리 다수
        for(float timer = 0; timer < 30f; timer += 0.3f) {
            Spawn(enemyData[6]); // 탕구리
            yield return new WaitForSeconds(0.3f);
        }
    }

    private IEnumerator BossSpawn() {
        // 시간 별 보스 스폰
        yield return new WaitForSeconds(60f);

        // 1 보스 : 대형 박쥐
        SpawnBoss(bossData[0]);
        yield return new WaitForSeconds(60f);
        
        // 2 보스 : 대형 리자드
        SpawnBoss(bossData[1]);
        yield return new WaitForSeconds(30f);
        
        // 3 보스 : 스컬
        SpawnBoss(bossData[2]);
        yield return new WaitForSeconds(60f);
        
        // 4 보스 : 대형 선인장
        SpawnBoss(bossData[3]);
        yield return new WaitForSeconds(60f);
        
        // 5 보스 : 대형 전갈
        SpawnBoss(bossData[4]);
        yield return new WaitForSeconds(30f);
        
        // 6 보스 : 트롤
        SpawnBoss(bossData[5]);
        yield return new WaitForSeconds(75f);
        
        // 7 보스 : 대형 거북맨
        SpawnBoss(bossData[6]);
        yield return new WaitForSeconds(75f);
        
        // 8 보스 : 악마 전사
        SpawnBoss(bossData[7]);
        yield return new WaitForSeconds(60f);
        
        // 9 보스 : 대형 물정령
        SpawnBoss(bossData[8]);
        yield return new WaitForSeconds(60f);
        
        // 10 보스 : 대형 탕구리
        SpawnBoss(bossData[9]); // 탕구리
        yield return new WaitForSeconds(30f);
        
        // 11 보스 : 도플갱어
        SpawnBoss(bossData[8]); // 도플갱어
    }

    private void Spawn(EnemyData enemyData) {
        GameObject enemy = ObjectPoolManager.Instance.GetPooledObject(0);
        if(enemy != null) {
            Vector2 randomPos = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
            enemy.GetComponent<EnemyManager>().Init(randomPos, enemyData);
        }
    }

    private void SpawnBoss(EnemyData enemyData) {
        GameObject enemy = ObjectPoolManager.Instance.GetPooledObject(0);

        if(enemy != null) {
            Vector2 randomPos = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
            enemy.GetComponent<EnemyManager>().InitBoss(randomPos, enemyData);
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
    public string name;
    public float health;
    public float damage;
    public float speed;
    public Vector2 colSize;
    public Vector2 scale;
}

[System.Serializable]
public class BossData {
    public int id;
    public string name;
    public float health;
    public float damage;
    public float speed;
    public Vector2 colSize;
    public Vector2 scale;
}
