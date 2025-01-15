using UnityEngine;

public class EssenceOfHealth : MonoBehaviour, IArtifact
{
    private PlayerHealth playerHealth;

    private void Start() {
        playerHealth = GetComponentInParent<PlayerHealth>();
    }

    public void Ability() {
        GameManager.Instance.playerData.hp += 0.1f;
        GameManager.Instance.playerData.genHp += 0.1f;
        playerHealth.SetMaxHp();
    }   
}
