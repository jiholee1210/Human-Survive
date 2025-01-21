using System.Collections;
using UnityEngine;

public class Lizard : MonoBehaviour, IMonster
{
    [SerializeField] private GameObject spearObject;
    [SerializeField] private LayerMask targetLayer; // 플레이어 레이저 탐지

    public float scanRange = 5f;
    public RaycastHit2D playertarget;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool GetNearPlayer() {
        if (playertarget.transform != null) {
            return true;
        }
        return false;
    }

    public void Ability() {
        StartCoroutine(SpawnSpear());
    }

    private IEnumerator SpawnSpear() {
        while (true) {
            playertarget = Physics2D.CircleCast(transform.position, scanRange, Vector2.zero, 0, targetLayer);
            if(GetNearPlayer()) {
                GameObject spear = Instantiate(spearObject, transform.position, Quaternion.identity);
                spear.GetComponent<LizardSpear>().Init();
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
