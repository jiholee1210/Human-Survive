using UnityEngine;

public class CoinBag : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer;

    //게임매니저 플레이어 데이터에 10골드씩 추가하는 기능
    private void OnTriggerEnter2D(Collider2D other) {
        if (((1 << other.gameObject.layer) & playerLayer) != 0) {
            GameManager.Instance.SetGold(10);
            Destroy(gameObject);
        }
    }
}
