using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<Item> weapon = new List<Item>();
    private List<Item> artifact = new List<Item>();
    private WeaponManager weaponManager;
    private ItemManager itemManager;

    private void Start() {
        weaponManager = GetComponentInChildren<WeaponManager>();
        itemManager = GetComponent<ItemManager>();
    }

    public void AddItem(Item mItem) {
        foreach (Item item in weapon) {
            if (mItem.itemId == item.itemId) {
                LevelUp(item);
                weaponManager.SpawnItem(item);
                return;
            }
        }
        Item newItem = mItem.CreateCopy(GameManager.Instance.playerData);
        weapon.Add(newItem);
        weaponManager.SpawnItem(newItem);
        Debug.Log("아이템 추가 : " + newItem.itemName);
    }

    public void AddArtifact(Item mItem) {
        Item newItem = mItem.CreateCopy(GameManager.Instance.playerData);
        artifact.Add(newItem);
        itemManager.ApplyArtifact(newItem);
        Debug.Log("아이템 추가 : " + newItem.itemName);
    }

    public void LevelUp(Item mItem) {
        mItem.itemLevel++;
        mItem.baseDamage += mItem.dmgUp[mItem.itemLevel-2];
        mItem.baseCount += mItem.countUp[mItem.itemLevel-2];
    }

    public List<Item> GetWeapons() {
        return weapon;
    }

    public List<Item> GetArtifacts() {
        return artifact;
    }

    public Item GetWeapon(int id) {
        foreach(Item item in weapon) {
            if (item.itemId == id) {
                return item;
            }
        }
        return null;
    }

    public Item GetArtifact(int id) {
        foreach(Item item in artifact) {
            if (item.itemId == id) {
                return item;
            }
        }
        return null;
    }

    public bool HaveWeapon(Item mItem) {
        foreach(Item item in weapon) {
            if(mItem.itemId == item.itemId) {
                return true;
            }
        }
        return false;
    }

    public bool HaveArtifact(Item mItem) {
        foreach(Item item in artifact) {
            if(mItem.itemId == item.itemId) {
                return true;
            }
        }
        return false;
    }
}
