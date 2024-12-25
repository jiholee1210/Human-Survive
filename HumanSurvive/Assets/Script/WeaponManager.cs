using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] GameObject[] weapons;

    public void SpawnItem(Item mItem) {
        weapons[mItem.itemId].GetComponent<ISpawner>().Init(mItem);
    }

    public void ItemLevelUp(Item mItem) {
        weapons[mItem.itemId].GetComponent<ISpawner>().LevelUp(mItem);
    }
}
