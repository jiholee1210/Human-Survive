using System.Collections;
using UnityEngine;

public class Meat : MonoBehaviour, IDrop
{
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Transform player;

    private Coroutine followCoroutine;


    public void Get(Transform playerTransform) {
        if(followCoroutine == null) {
            player = playerTransform;
            followCoroutine = StartCoroutine(FollowPlayer());
        }
    }

    private IEnumerator FollowPlayer() {
        while(true) {
            transform.position = Vector3.MoveTowards(transform.position, player.position, 8f * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (((1 << other.gameObject.layer) & playerLayer) != 0) {
            // 플레이어와 충돌했을 때의 처리
            GameManager.Instance.player.GetComponent<PlayerHealth>().RestoreHp(10f);
            Destroy(gameObject);
        }
    }
}
