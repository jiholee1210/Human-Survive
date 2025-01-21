using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArtifactSelector : MonoBehaviour
{
    [SerializeField] private Item[] artifacts;
    [SerializeField] private Button itemSlot;
    [SerializeField] private Button passBtn;
    [SerializeField] private PlayerInventory playerInventory;

    public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable() {
        InitButton();
    }
    
    private void Awake() {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        itemSlot.onClick.AddListener(() => {
            if(GameManager.Instance.canClick) {
                Item selectedArtifact = itemSlot.GetComponent<ItemButton>().item;
                playerInventory.AddArtifact(selectedArtifact);
                GameManager.Instance.CloseArtifact();
            }
        });

        passBtn.onClick.AddListener(() => {
            if(GameManager.Instance.canClick) {
                GameManager.Instance.CloseArtifact();
            }
        });
    }

    private void InitButton() {
        SetArtifactSlot(itemSlot);
    }

    private void SetArtifactSlot(Button button) {
        int randomIndex = Random.Range(0, artifacts.Length);
        Item selectedArtifact = artifacts[randomIndex];

        Image image = button.transform.GetChild(1).GetComponent<Image>();
        TMP_Text name = button.transform.GetChild(2).GetComponent<TMP_Text>();
        TMP_Text desc = button.transform.GetChild(3).GetComponent<TMP_Text>();

        image.sprite = selectedArtifact.itemSprite;
        name.text = selectedArtifact.itemName;
        desc.text = selectedArtifact.itemDesc;

        button.GetComponent<ItemButton>().item = selectedArtifact;
    }
}
