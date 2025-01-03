using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public enum ItemType {
    None = 0,
    Weapon = 1,
    Buff = 2,

    Melee = 10,
    Range = 11,

}

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObject/ItemData")]
public class Item : ScriptableObject
{
    [Header("아이템 Info")]
    [SerializeField] public int itemId;
    [SerializeField] public int prefabId;
    [SerializeField] public ItemType itemType;
    [SerializeField] public string itemName;
    [SerializeField] public string itemDesc;
    [SerializeField] public Sprite itemSprite;
    [SerializeField] public RuntimeAnimatorController animCon;
    [SerializeField] public bool canOverlap;

    [Header("무기 Info")]
    [SerializeField] public int itemLevel;
    [SerializeField] public float baseDamage;
    [SerializeField] public int baseCount;
    [SerializeField] public float[] dmgUp;
    [SerializeField] public int[] countUp;
    [SerializeField] public float coolDown;
    [SerializeField] public float range;
    [SerializeField] public bool isGuided;

    public Item CreateCopy() {
        Item copy = ScriptableObject.CreateInstance<Item>();
        copy.itemLevel = itemLevel;
        copy.itemId = this.itemId;
        copy.prefabId = this.prefabId;
        copy.itemType = this.itemType;
        copy.itemName = this.itemName;
        copy.itemDesc = this.itemDesc;
        copy.itemSprite = this.itemSprite;
        copy.animCon = this.animCon;
        copy.canOverlap = this.canOverlap;
        copy.baseDamage = this.baseDamage;
        copy.baseCount = this.baseCount;
        copy.dmgUp = (float[])this.dmgUp.Clone();
        copy.countUp = (int[])this.countUp.Clone();
        copy.coolDown = this.coolDown;
        copy.range = this.range;
        copy.isGuided = this.isGuided;
        return copy;
    }
}
