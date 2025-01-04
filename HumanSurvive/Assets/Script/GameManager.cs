using System.Collections;
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
    [SerializeField] Slider expBar;
    [SerializeField] GameObject itemSelect;

    public int killCount;
    private float time;

    private void Awake() {
        Instance = this;

        player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerManager>().Init();
        Init();

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

    private IEnumerator OpenItemSelectAfterDelay() {
        
        yield return new WaitForSeconds(0.1f);
        // 애니메이션이 재생되도록 설정
        Time.timeScale = 0;
        OpenItemSelect();
        
   }

    private void OpenItemSelect() {
        itemSelect.SetActive(true);
        itemSelect.GetComponent<Animator>().SetBool("Open", true);
    }

    public void CloseItemSelect() {
        itemSelect.GetComponent<Animator>().SetBool("Open", false);
        StartCoroutine(WaitForAnimation(itemSelect.GetComponent<Animator>(), "Selector_close"));
    }

    private IEnumerator WaitForAnimation(Animator animator, string animationStateName) {
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
        Time.timeScale = 1f;
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
