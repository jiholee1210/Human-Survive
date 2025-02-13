using UnityEngine;

public class ItemScanner : MonoBehaviour
{
    [SerializeField] LayerMask itemLayer;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & itemLayer) != 0) {
            other.GetComponent<IDrop>().Get(transform);
        }
    }
}
