using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<Item> inventory = new List<Item>();

    public void AddItem(Item item) {
        inventory.Add(item);
        Debug.Log("아이템 추가 : " + item.itemName);
    }
}
