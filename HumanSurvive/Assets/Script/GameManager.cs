using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    public GameObject player;
    
    [SerializeField] TMP_Text killCountText;
    [SerializeField] TMP_Text timeText;
    [SerializeField] TMP_Text levelText;
    [SerializeField] TMP_Text goldText;
    [SerializeField] Slider expBar;
    [SerializeField] GameObject itemSelect;
    [SerializeField] GameObject gameEndObject;
    [SerializeField] GameObject itemIconPrefab;
    [SerializeField] Button menuBtn;
    [SerializeField] Button restartBtn;
    [SerializeField] GameObject artifactSelect;

    public int killCount;
    public float time;
    public bool canClick;
    public bool isOpen;

    [SerializeField] public PlayerData playerData;

    private void Awake() {
        Instance = this;
        Debug.Log("게임 매니저 초기세팅");
        player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerManager>().Init();
        Init();

        playerData = DataManager.Instance.playerData.Clone();
        canClick = false;
        isOpen = false;
        StartCoroutine(OpenItemSelectAfterDelay());
    }

    private void FixedUpdate() {
        time += Time.deltaTime;
        SetTimeText();
    }

    public void SetKillCount(int kill) {
        killCount += kill;
        SetKillCountText();
    }

    private void SetKillCountText() {
        killCountText.text = killCount.ToString();
    }

    private void SetTimeText() {
        int min = Mathf.FloorToInt(time / 60);
        int sec = Mathf.FloorToInt(time % 60);
        timeText.text = string.Format("{0:D2}:{1:D2}", min, sec);
    }

    private void SetLevelText() {
        levelText.text = "Lv. " + player.GetComponent<PlayerManager>().level.ToString();
    }

    public void SetPlayerExp(float exp) {
        int isLevelUp = player.GetComponent<PlayerManager>().GetExp(exp);
        SetExpValue();
        if (isLevelUp > 0) {
            SetLevelText();
            Time.timeScale = 0;
            OpenItemSelect();
        }
    }

    private void SetExpValue() {
        expBar.maxValue = player.GetComponent<PlayerManager>().maxExp;
        expBar.minValue = 0;
        expBar.value = player.GetComponent<PlayerManager>().curExp;
    }

    public void SetGold(int gold) {
        playerData.gold += gold;
        goldText.text = playerData.gold.ToString();
    }

    private IEnumerator OpenItemSelectAfterDelay() {
        
        yield return new WaitForSeconds(0.1f);
        // 애니메이션이 재생되도록 설정
        Time.timeScale = 0;
        OpenItemSelect();
        
   }

    private void OpenItemSelect() {
        itemSelect.SetActive(true);
        itemSelect.GetComponent<Animator>().SetBool("Open", true);
        StartCoroutine(WaitForAnimation(itemSelect, itemSelect.GetComponent<Animator>(), "Selector_open"));
    }

    public void CloseItemSelect() {
        itemSelect.GetComponent<Animator>().SetBool("Open", false);
        StartCoroutine(WaitForAnimation(itemSelect, itemSelect.GetComponent<Animator>(), "Selector_close"));
    }

    public void OpenArtifact() {
        artifactSelect.SetActive(true);
        artifactSelect.GetComponent<Animator>().SetBool("Open", true);
        StartCoroutine(WaitForAnimation(artifactSelect, artifactSelect.GetComponent<Animator>(), "Artifact_open"));
    }

    public void CloseArtifact() {
        artifactSelect.GetComponent<Animator>().SetBool("Open", false);
        StartCoroutine(WaitForAnimation(artifactSelect, artifactSelect.GetComponent<Animator>(), "Artifact_close"));
    }

    private IEnumerator WaitForAnimation(GameObject selector, Animator animator, string animationStateName) {
        canClick = false;
        // 애니메이션이 끝날 때까지 대기
        while (true) {
            // 현재 애니메이션 상태 정보 가져오기
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            // 애니메이션이 끝났는지 확인
            if (stateInfo.IsName(animationStateName) && stateInfo.normalizedTime >= 1.0f) {
                break; // 애니메이션이 끝났으면 루프 종료
            }
            yield return null; // 다음 프레임까지 대기
        }
        if(isOpen) {
            Time.timeScale = 1f;
            selector.SetActive(false);
        }
        isOpen = !isOpen;
        canClick = true;
    }

    private void Init() {
        killCount = 0;
        time = 0f;
        playerData.gold = 0;

        SetKillCountText();
        SetTimeText();
        SetLevelText();
        SetExpValue();
        SetGold(0);
    }

    public void GameEnd() {
        Time.timeScale = 0f;
        ShowEndUI();
    }

    private void ShowEndUI() {
        gameEndObject.SetActive(true);
        // 생존시간 출력
        TMP_Text record = gameEndObject.transform.GetChild(1).GetComponent<TMP_Text>();
        int min = Mathf.FloorToInt(time / 60);
        int sec = Mathf.FloorToInt(time % 60);
        record.text = string.Format("{0:D2}:{1:D2}", min, sec);
        // 소지중인 아이템 나열
        List<Item> inventory = player.GetComponent<PlayerInventory>().GetWeapons();
        for(int i = 0; i < inventory.Count; i++) {
            GameObject icon = Instantiate(itemIconPrefab, gameEndObject.transform);
            ItemIcon itemIcon = icon.GetComponent<ItemIcon>();
            itemIcon.item = inventory[i];

            RectTransform rectTransform = icon.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(-10 * (inventory.Count - 1) + (i * 20), 0);
        }
        // 골드 획득량 원본 데이터로 넘기기
        DataManager.Instance.playerData.gold = playerData.gold;
        // 메인메뉴 / 재시작 버튼 활성화
        menuBtn.onClick.AddListener(() => {
            BackToMenu();
        });
        restartBtn.onClick.AddListener(() => {
            Restart();
        });
    }

    private void BackToMenu() {
        Time.timeScale = 1f;
        gameEndObject.SetActive(false);
        SceneSelector.Instance.LoadMain();
    }
    private void Restart() {
        Time.timeScale = 1f;
        gameEndObject.SetActive(false);
        SceneSelector.Instance.LoadGame();
    }
}
