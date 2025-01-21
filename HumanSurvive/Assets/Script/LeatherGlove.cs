using UnityEngine;

public class LeatherGlove : MonoBehaviour, IArtifact
{
    public void Ability() {
        GameManager.Instance.playerData.attackSpeed += 0.25f;
    }
}
