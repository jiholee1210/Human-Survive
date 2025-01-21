using System;
using TMPro;
using Unity.Mathematics;
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
    [SerializeField] TMP_Text gold;
    [SerializeField] TMP_Text[] reqGold;


    [SerializeField] GameObject upgradePanel;

    private PlayerData playerData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerData = DataManager.Instance.playerData;

        startBtn.onClick.AddListener(() => {
            SceneSelector.Instance.LoadGame();
        });
        endBtn.onClick.AddListener(() => {
            SceneSelector.Instance.ExitGame();
        });
        upgradeBtn.onClick.AddListener(() => {
            upgradePanel.SetActive(true);
            SetUpgradeStat();
            SetGoldText();
            for (int i = 0; i < reqGold.Length; i++) {
                SetReqGold(i, (int)(10 * Math.Pow(2, playerData.upgrade[i])));
            }
        });
        exitBtn.onClick.AddListener(() => {
            upgradePanel.SetActive(false);
        });
        for(int i = 0; i < upBtn.Length; i++) {
            int index = i;
            upBtn[index].onClick.AddListener(() => {
                if(playerData.gold < (int)(10 * Math.Pow(2, playerData.upgrade[index]))) {
                    Debug.Log("골드가 부족합니다.");
                    return;
                }
                Debug.Log(index);
                Debug.Log(index + "번째 업그레이드 클릭 : " + playerData.upgrade[index]);
                playerData.gold -= (int)(10 * Math.Pow(2, playerData.upgrade[index]));
                SetGoldText();
                playerData.upgrade[index]++;
                SetReqGold(index, (int)(10 * Math.Pow(2, playerData.upgrade[index])));
                stats[index].text = playerData.upgrade[index].ToString();
                DataManager.Instance.SavePlayerData();
            });
        }
        for(int i = 0; i < downBtn.Length; i++) {
            int index = i;
            downBtn[index].onClick.AddListener(() => {
                if(playerData.upgrade[index] <= 0) {
                    Debug.Log("현재 0레벨 입니다.");
                    return;
                }
                Debug.Log(index);
                Debug.Log(index + "번째 업그레이드 클릭 : " + playerData.upgrade[index]);
                playerData.upgrade[index]--;
                playerData.gold += (int)(10 * Math.Pow(2, playerData.upgrade[index]));
                SetGoldText();
                SetReqGold(index, (int)(10 * Math.Pow(2, playerData.upgrade[index])));
                stats[index].text = playerData.upgrade[index].ToString();
                DataManager.Instance.SavePlayerData();
            });
        }
    }

    private void SetUpgradeStat() {
        for(int i = 0; i < stats.Length; i++) {
            int index = i;
            stats[index].text = playerData.upgrade[index].ToString();
        }
    }
    
    private void SetGoldText() {
        gold.text = playerData.gold.ToString();
    }

    private void SetReqGold(int index, int gold) {
        reqGold[index].text = gold.ToString() + " 골드";
    }
}
