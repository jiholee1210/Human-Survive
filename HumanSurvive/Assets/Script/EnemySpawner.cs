using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float spawnSpeed;

    string[] enemyName = {"Bat"};

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(GenTimer());
    }

    private IEnumerator GenTimer() {
        while (true) {
            EnemySpawn();
            yield return new WaitForSeconds(spawnSpeed);
        }
    }

    private void EnemySpawn() {
        int randomIndex = Random.Range(0, enemyName.Length);
        string prefabName = enemyName[randomIndex];
        
        GameObject enemy = ObjectPoolManager.Instance.GetPooledObject(prefabName);
        if(enemy != null) {
            Vector2 randomPos = new Vector2(10f, 5f);

            enemy.GetComponent<EnemyManager>().Init(randomPos);
        }
    }
}
