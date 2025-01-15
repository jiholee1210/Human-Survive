using UnityEngine;

public class EssenceOfSpeed : MonoBehaviour, IArtifact
{
    public void Ability() {
        GameManager.Instance.playerData.speed += 0.1f;
    }
}
