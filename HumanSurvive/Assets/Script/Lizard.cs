using System.Collections;
using UnityEngine;

public class Lizard : MonoBehaviour, IMonster
{
    [SerializeField] private GameObject spearObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Ability() {
        StartCoroutine(SpawnSpear());
    }

    private IEnumerator SpawnSpear() {
        while (true) {
            GameObject spear = Instantiate(spearObject, transform.position, Quaternion.identity);
            spear.GetComponent<LizardSpear>().Init();
            yield return new WaitForSeconds(1f);
        }
    }
}
