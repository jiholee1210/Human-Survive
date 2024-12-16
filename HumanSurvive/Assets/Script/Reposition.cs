using System;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    [SerializeField] LayerMask colLayer;

    private void OnTriggerExit2D(Collider2D other) {
        if (((1 << other.gameObject.layer) & colLayer) != 0) {
            Debug.Log("물체가 트리거 범위에서 나감" + gameObject.layer + "");
            switch (gameObject.layer) {
                case 6:
                    TileRepos();
                    break;
                case 7:
                    Debug.Log("적이 벗어남");
                    EnemyRepos();
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
        if(Mathf.Abs(distY) >= 20   ) {
            transform.Translate(Vector2.up * Math.Sign(distY) * 40);
        }
    }

    private void EnemyRepos() {
        Vector2 playerDir = GameManager.Instance.player.GetComponent<PlayerMovement>().inputVec;

        transform.Translate(playerDir * 20 + new Vector2(UnityEngine.Random.Range(0.5f, 1f), UnityEngine.Random.Range(0.5f, 1f)));
    }
}
