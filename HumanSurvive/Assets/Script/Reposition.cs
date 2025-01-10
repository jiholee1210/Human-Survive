using System;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    [SerializeField] LayerMask colLayer;

    private EnemyManager enemyManager;


    private void OnTriggerExit2D(Collider2D other) {
        if (((1 << other.gameObject.layer) & colLayer) != 0) {
            switch (gameObject.layer) {
                case 6:
                    TileRepos();
                    break;
                case 7:
                    enemyManager = GetComponent<EnemyManager>();
                    if(enemyManager.isHorde) {
                        enemyManager.Die();
                    }
                    else {
                        EnemyRepos();
                    }
                    break;
            }   
        }
    }

    private void TileRepos() {  
        Vector2 playerPos = GameManager.Instance.player.transform.position;
        Vector2 myPos = transform.position;

        float distX = playerPos.x - myPos.x;
        float distY = playerPos.y - myPos.y;
        if(Mathf.Abs(distX) >= 20) {
            transform.Translate(Vector2.right * Math.Sign(distX) * 40);
        } 
        if(Mathf.Abs(distY) >= 20) {
            transform.Translate(Vector2.up * Math.Sign(distY) * 40);
        }
    }

    private void EnemyRepos() {
        Vector2 playerDir = GameManager.Instance.player.GetComponent<PlayerMovement>().inputVec;
        Vector2 playerPos = GameManager.Instance.player.transform.position;
        float distance = Vector2.Distance(playerPos, transform.position);
        transform.Translate(playerDir * distance * 2f);
    }
}
