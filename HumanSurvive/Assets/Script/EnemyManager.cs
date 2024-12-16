using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyManager : MonoBehaviour
{
    public IObjectPool<GameObject> pool {get; set;}

    private Rigidbody2D rigidbody2D;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Init(Vector2 randomPos)
    {
        transform.position = randomPos;
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.linearVelocity = Vector2.zero;
    }

    private void Die() {
        pool.Release(gameObject);
    }
}
