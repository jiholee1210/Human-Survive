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

    public Animator animator;

    private void OnEnable() {
        InitButton();
    }

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        foreach (var button in itemSlots) {
            button.onClick.AddListener(() => {
                // 버튼을 누르면 해당 버튼에 저장된 Item 정보를 PlayerInventory로 넘김
                if(GameManager.Instance.canClick) {
                    Item selectedItem = button.GetComponent<ItemButton>().item;
                    playerInventory.AddItem(selectedItem);
                    GameManager.Instance.CloseItemSelect();
                }
            });
        }

        reRollBtn.onClick.AddListener(() => {
            InitButton();
        });

        passBtn.onClick.AddListener(() => {
            if(GameManager.Instance.canClick) {
                GameManager.Instance.CloseItemSelect();
            }
        });
    }

    private void InitButton() {
        foreach (var button in itemSlots) {
            SetItemSlot(button);
        }
    }

    private void SetItemSlot(Button button) {
        int randomIndex = Random.Range(0, items.Length);
        // 인벤토리에 아이템이 존재할 때와 처음 획득할 때를 구분해서 레벨업 기능 구현해야 함.
        Item selectedItem = items[randomIndex];

        Image image = button.transform.GetChild(0).GetComponent<Image>();
        TMP_Text name = button.transform.GetChild(1).GetComponent<TMP_Text>();
        TMP_Text type = button.transform.GetChild(2).GetComponent<TMP_Text>();
        TMP_Text dmg = button.transform.GetChild(3).GetComponent<TMP_Text>();
        TMP_Text cool = button.transform.GetChild(4).GetComponent<TMP_Text>();
        TMP_Text level = button.transform.GetChild(5).GetComponent<TMP_Text>();

        image.sprite = selectedItem.itemSprite;
        name.text = selectedItem.itemName;
        type.text = "타입\t: " + selectedItem.weaponType.ToString();
        if (playerInventory.HaveItem(selectedItem)) {
            Item item = playerInventory.GetItem(selectedItem.itemId);
            level.text = "레벨 " + item.itemLevel + " -> " + (item.itemLevel+1);
            dmg.text = "데미지\t: " + item.baseDamage + " + " + item.dmgUp[selectedItem.itemLevel-1];
        }
        else {
            level.text = "신규!";
            dmg.text = "데미지\t: " + selectedItem.baseDamage.ToString();
        }
        string cooldown = selectedItem.coolDown == -1 ? "X" : selectedItem.coolDown.ToString();
        cool.text = "쿨타임\t: " + cooldown;

        button.GetComponent<ItemButton>().item = selectedItem;
    }


}
