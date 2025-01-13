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
        Item newItem = mItem.CreateCopy(GameManager.Instance.playerData);
        inventory.Add(newItem);
        weaponManager.SpawnItem(newItem);
        Debug.Log("아이템 추가 : " + newItem.itemName);
    }

    public void AddArtifact(Item mItem) {
        Item newItem = mItem.CreateCopy(GameManager.Instance.playerData);
        inventory.Add(newItem);
        Debug.Log("아이템 추가 : " + newItem.itemName);
    }

    public void LevelUp(Item mItem) {
        mItem.itemLevel++;
        mItem.baseDamage += mItem.dmgUp[mItem.itemLevel-2];
        mItem.baseCount += mItem.countUp[mItem.itemLevel-2];
    }

    public List<Item> GetItems() {
        return inventory;
    }

    public Item GetItem(int id) {
        foreach(Item item in inventory) {
            if (item.itemId == id) {
                return item;
            }
        }
        return null;
    }

    public bool HaveItem(Item mItem) {
        foreach(Item item in inventory) {
            if(mItem.itemId == item.itemId) {
                return true;
            }
        }
        return false;
    }
}
