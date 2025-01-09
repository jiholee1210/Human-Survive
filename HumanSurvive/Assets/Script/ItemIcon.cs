using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] public Item item;
    [SerializeField] private GameObject toolTip;

    private Image image;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<Image>();
        toolTip = transform.GetChild(0).gameObject;
        image.sprite = item.itemSprite;
        toolTip.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        toolTip.SetActive(true);
        TMP_Text level = toolTip.transform.GetChild(0).GetComponent<TMP_Text>();
        TMP_Text type = toolTip.transform.GetChild(1).GetComponent<TMP_Text>();
        TMP_Text dmg = toolTip.transform.GetChild(2).GetComponent<TMP_Text>();
        TMP_Text dps = toolTip.transform.GetChild(3).GetComponent<TMP_Text>();

        level.text = "레벨 " + item.itemLevel;
        type.text = "타입\t: " + item.weaponType;
        dmg.text = "데미지\t: " + item.baseDamage;
        dps.text = "DPS\t: 0";
    }

    public void OnPointerExit(PointerEventData eventData) {
        toolTip.SetActive(false);
    }
}
