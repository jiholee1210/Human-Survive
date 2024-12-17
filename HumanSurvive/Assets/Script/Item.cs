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
    [SerializeField] public Sprite itemSprite;
    [SerializeField] public RuntimeAnimatorController animCon;
    [SerializeField] public bool canOverlap;

    [Header("무기 Info")]
    [SerializeField] public float baseDamage;
    [SerializeField] public int baseCount;
    [SerializeField] public float[] dmgUp;
    [SerializeField] public int[] countUp;
    [SerializeField] public float coolDown;
    [SerializeField] public float range;
    [SerializeField] public bool isGuided;
}
