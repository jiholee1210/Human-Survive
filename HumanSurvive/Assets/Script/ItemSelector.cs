using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelector : MonoBehaviour
{
    [SerializeField] private Item[] items;
    [SerializeField] private Button[] itemSlots;
    [SerializeField] private Button reRollBtn;
    [SerializeField] private Button passBtn;
    [SerializeField] private PlayerInventory playerInventory;

    private void OnEnable() {
        InitButton();
    }

    private void Start() {
        foreach (var button in itemSlots) {
            button.onClick.AddListener(() => {
                // 버튼을 누르면 해당 버튼에 저장된 Item 정보를 PlayerInventory로 넘김
                Item selectedItem = button.GetComponent<ItemButton>().item;
                playerInventory.AddItem(selectedItem);
                Time.timeScale = 1f;
                GameManager.Instance.CloseItemSelect();
            });
        }

        reRollBtn.onClick.AddListener(() => {
            foreach(var button in itemSlots) {
                SetItemSlot(button);
            }
        });

        passBtn.onClick.AddListener(() => {
            Time.timeScale = 1f;
            GameManager.Instance.CloseItemSelect();
        });
    }

    private void InitButton() {
        foreach (var button in itemSlots) {
            SetItemSlot(button);
        }
    }

    private void SetItemSlot(Button button) {
        int randomIndex = Random.Range(0, items.Length);
        Item selectedItem = items[randomIndex];

        Image image = button.transform.GetChild(0).GetComponent<Image>();
        TMP_Text name = button.transform.GetChild(1).GetComponent<TMP_Text>();
        TMP_Text desc = button.transform.GetChild(2).GetComponent<TMP_Text>();

        image.sprite = selectedItem.itemSprite;
        name.text = selectedItem.itemName;
        desc.text = selectedItem.itemDesc;

        button.GetComponent<ItemButton>().item = selectedItem;
    }


}
