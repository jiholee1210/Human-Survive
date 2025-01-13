using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;

    private void OnTriggerEnter2D(Collider2D other) {
        if(((1 << other.gameObject.layer) & playerLayer) != 0) {
            Time.timeScale = 0f;
            GameManager.Instance.OpenArtifact();
            Destroy(gameObject);
        }
    }
}
