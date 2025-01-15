using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Button startBtn;
    [SerializeField] Button endBtn;
    [SerializeField] Button upgradeBtn;
    [SerializeField] Button exitBtn;
    [SerializeField] Button[] upBtn;
    [SerializeField] Button[] downBtn;
    [SerializeField] TMP_Text[] stats;


    [SerializeField] GameObject upgradePanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startBtn.onClick.AddListener(() => {
            SceneSelector.Instance.LoadGame();
        });
        endBtn.onClick.AddListener(() => {
            SceneSelector.Instance.ExitGame();
        });
        upgradeBtn.onClick.AddListener(() => {
            upgradePanel.SetActive(true);
            SetUpgradeStat();
        });
        exitBtn.onClick.AddListener(() => {
            upgradePanel.SetActive(false);
        });
        for(int i = 0; i < upBtn.Length; i++) {
            int index = i;
            upBtn[index].onClick.AddListener(() => {
                Debug.Log(index);
                Debug.Log(index + "번째 업그레이드 클릭 : " + DataManager.Instance.playerData.upgrade[index]);
                DataManager.Instance.playerData.upgrade[index]++;
                stats[index].text = DataManager.Instance.playerData.upgrade[index].ToString();
                DataManager.Instance.SavePlayerData();
            });
        }
        for(int i = 0; i < downBtn.Length; i++) {
            int index = i;
            downBtn[index].onClick.AddListener(() => {
                Debug.Log(index);
                Debug.Log(index + "번째 업그레이드 클릭 : " + DataManager.Instance.playerData.upgrade[index]);
                DataManager.Instance.playerData.upgrade[index]--;
                stats[index].text = DataManager.Instance.playerData.upgrade[index].ToString();
                DataManager.Instance.SavePlayerData();
            });
        }
    }

    private void SetUpgradeStat() {
        for(int i = 0; i < stats.Length; i++) {
            int index = i;
            stats[index].text = DataManager.Instance.playerData.upgrade[index].ToString();
        }
    }
}
