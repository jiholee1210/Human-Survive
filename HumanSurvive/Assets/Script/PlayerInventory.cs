using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<Item> inventory = new List<Item>();

    private WeaponManager weaponManager;

    private void Start() {
        weaponManager = GetComponentInChildren<WeaponManager>();
    }

    public void AddItem(Item mItem) {
        foreach (Item item in inventory) {
            if (mItem.itemId == item.itemId) {
                LevelUp(item);
                weaponManager.SpawnItem(item);
                return;
            }
        }
        Item newItem = mItem.CreateCopy();
        inventory.Add(newItem);
        weaponManager.SpawnItem(newItem);
        Debug.Log("아이템 추가 : " + newItem.itemName);
    }

    public void LevelUp(Item mItem) {
        mItem.itemLevel++;
        mItem.baseCount += mItem.countUp[mItem.itemLevel-2];
    }

    public List<Item> GetItem() {
        return inventory;
    }

    public bool HaveItem(Item mItem) {
        return inventory.Contains(mItem);
    }
}
