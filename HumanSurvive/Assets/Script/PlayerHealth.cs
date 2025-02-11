using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] float maxHp;
    [SerializeField] float currentHp;
    [SerializeField] float defaultHp;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] LayerMask bossAttackLayer;
    [SerializeField] Slider HpBar;

    private PlayerManager playerManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        defaultHp = 100f * (1 + GameManager.Instance.playerData.hp + (GameManager.Instance.playerData.upgrade[0] * 0.1f));
        maxHp = defaultHp;
        currentHp = defaultHp;
        playerManager = GetComponent<PlayerManager>();
        StartCoroutine(RegenHp());
    }

    private void FixedUpdate() {
        if (currentHp <= 0) {
            Die();
        }

        if (HpBar != null) {
               HpBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 0.8f, 0));
        } else {
            Debug.LogError("HpBar가 null입니다!");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMaxHp() {
        maxHp = defaultHp * (1 + GameManager.Instance.playerData.hp);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (((1 << other.gameObject.layer) & enemyLayer) != 0) {
            EnemyAttack enemyAttack = other.gameObject.GetComponent<EnemyAttack>();
            OnHit(enemyAttack.GetDamage());
        }
    }

    public void OnHit(float damage)
    {
        currentHp -= damage;
        HpBar.value = currentHp / maxHp;
        Debug.Log("" + currentHp);
    }

    public void RestoreHp(float heal)
    {
        currentHp += heal;
        HpBar.value = currentHp / maxHp;
    }

    public IEnumerator RegenHp() {
        while(true) {
            if(currentHp < maxHp) {
                currentHp += GameManager.Instance.playerData.genHp + (GameManager.Instance.playerData.upgrade[1] * 0.1f);
                Debug.Log("체력 리젠" + currentHp);
                currentHp = Mathf.Min(currentHp, maxHp);
                HpBar.value = currentHp / maxHp;
            }
            yield return new WaitForSeconds(1f);
        }
    }

    public void Die()
    {
        playerManager.Die();
    }
}
