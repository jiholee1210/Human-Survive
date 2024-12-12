using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float xMax;
    [SerializeField] float yMax;
    [SerializeField] float xMin;
    [SerializeField] float yMin;
    [SerializeField] float spawnSpeed;

    private float first;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        first = 8f;
    }

    private IEnumerator EnemySpawn() {
        float tmp = Random.value > 0.5f ? 1f : -1f;
        float xPos = tmp * first;
        yield return new WaitForSeconds(spawnSpeed);
    }
}
