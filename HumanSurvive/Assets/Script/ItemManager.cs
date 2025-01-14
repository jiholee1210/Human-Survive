using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private PlayerInventory playerInventory;

    private void Start() {
        playerInventory = GetComponent<PlayerInventory>();
    }

    public void ApplyArtifact(Item mItem) {
        switch (mItem.itemId) {
            case 10:
                EssenceOfPower();
                break;
            case 11:
                break;
            case 12:
                break;
            case 13:
                break;
            case 14:
                break;
            case 15:
                break;
            case 16:
                break;
            case 17:
                break;
        }
    }

    private void EssenceOfPower() {
        foreach (Item weapon in playerInventory.GetWeapons()) {
            weapon.finalDamage += 0.25f;
        }
    }

    private void EssenceOfHealth() {

    }

    private void EssenceOfSpeed() {

    }

    private void Glove() {

    }
}
