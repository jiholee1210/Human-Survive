using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<Item> inventory = new List<Item>();

    private WeaponManager weaponManager;

    private void Start() {
        weaponManager = GetComponentInChildren<WeaponManager>();
    }

    public void AddItem(Item item) {
        inventory.Add(item);
        weaponManager.SpawnItem(item);
        Debug.Log("아이템 추가 : " + item.itemName);
    }

    public void LevelUp(Item mItem) {
        foreach (Item item in inventory) {
            if (mItem.itemId == item.itemId) {
                item.baseCount++;
                weaponManager.ItemLevelUp(item);
                break;
            }
        }
    }

    public List<Item> GetItem() {
        return inventory;
    }

    public bool HaveItem(Item mItem) {
        return inventory.Contains(mItem);
    }
}
