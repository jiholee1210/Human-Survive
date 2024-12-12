using System;
using UnityEngine;

public class TileReposition : MonoBehaviour
{
    [SerializeField] LayerMask colLayer;

    private void OnTriggerExit2D(Collider2D other) {
        if (((1 << other.gameObject.layer) & colLayer) != 0) {
            Debug.Log("플레이어 벗어남");
            Vector2 playerPos = GameManager.Instance.player.transform.position;
            Vector2 myPos = transform.position;

            float distX = playerPos.x - myPos.x;
            float distY = playerPos.y - myPos.y;
            Debug.Log(distX + " " + distY + "");
            if(Mathf.Abs(distX) >= 20) {
                transform.Translate(Vector2.right * Math.Sign(distX) * 40);
            } 
            if(Mathf.Abs(distY) >= 20   ) {
                transform.Translate(Vector2.up * Math.Sign(distY) * 40);
            } 
        }
    }
}
