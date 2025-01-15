using System.Collections.Generic;
using UnityEngine;

public class EssenceOfPower : MonoBehaviour, IArtifact
{
    public void Ability() {
        GameManager.Instance.playerData.finalDamage += 0.25f;
    }
}