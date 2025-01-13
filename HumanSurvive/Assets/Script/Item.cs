using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public enum ItemType {
    None = 0,
    Weapon = 1,
    Artifact = 2,

    Melee = 10,
    Range = 11,
}

public enum WeaponType {
    None = 0,
    Bullet = 1,
    Cycle = 2,
    Swing = 3,
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
    [SerializeField] public WeaponType weaponType;
    [SerializeField] public float baseDamage;
    [SerializeField] public int baseCount;
    [SerializeField] public float[] dmgUp;
    [SerializeField] public int[] countUp;
    [SerializeField] public float coolDown;
    [SerializeField] public float range;
    [SerializeField] public float speed;
    [SerializeField] public bool isGuided;

    public Item CreateCopy(PlayerData playerData) {
        Item copy = ScriptableObject.CreateInstance<Item>();
        copy.itemLevel = itemLevel;
        copy.weaponType = weaponType;
        copy.itemId = this.itemId;
        copy.prefabId = this.prefabId;
        copy.itemType = this.itemType;
        copy.itemName = this.itemName;
        copy.itemDesc = this.itemDesc;
        copy.itemSprite = this.itemSprite;
        copy.animCon = this.animCon;
        copy.canOverlap = this.canOverlap;
        copy.baseDamage = this.baseDamage;
        copy.baseCount = this.baseCount + playerData.stat[5];
        copy.speed = this.speed + playerData.stat[3];
        copy.dmgUp = (float[])this.dmgUp.Clone();
        copy.countUp = (int[])this.countUp.Clone();
        copy.coolDown = this.coolDown;
        copy.range = this.range + playerData.stat[4];
        copy.isGuided = this.isGuided;
        return copy;
    }
}
