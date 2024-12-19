using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    public GameObject player;
    
    [SerializeField] TMP_Text killCountText;
    [SerializeField] TMP_Text timeText;
    [SerializeField] TMP_Text levelText;
    [SerializeField] Slider expBar;
    [SerializeField] GameObject itemSelect;

    public int killCount;
    private float time;

    private void Awake() {
        Instance = this;
        player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerManager>().Init();
        Init();
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

    private void OpenItemSelect() {
        itemSelect.SetActive(true);
    }

    public void CloseItemSelect() {
        itemSelect.SetActive(false);
    }

    private void Init() {
        killCount = 0;
        time = 0f;

        SetKillCountText();
        SetTimeText();
        SetLevelText();
        SetExpValue();
    }

    public void GameEnd() {

    }
}
