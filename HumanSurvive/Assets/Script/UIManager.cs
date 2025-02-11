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
    [SerializeField] Button resetBtn;
    [SerializeField] Button[] upBtn;
    [SerializeField] GameObject[] upgradeSlot;
    [SerializeField] TMP_Text gold;
    [SerializeField] TMP_Text[] reqGold;


    [SerializeField] GameObject upgradePanel;

    private PlayerData playerData;
    private int[] upgrade = {5, 3, 3, 5, 5, 2, 3, 2};

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
            SetSlot();
            SetGoldText();
            for (int i = 0; i < reqGold.Length; i++) {
                SetReqGold(i, (int)(10 * Math.Pow(2, playerData.upgrade[i])));
            }
        });
        exitBtn.onClick.AddListener(() => {
            upgradePanel.SetActive(false);
        });
        resetBtn.onClick.AddListener(() => {
            Reset();
        });
        for(int i = 0; i < upBtn.Length; i++) {
            int index = i;
            upBtn[index].onClick.AddListener(() => {
                if(playerData.gold < (int)(10 * Math.Pow(2, playerData.upgrade[index]))) {
                    Debug.Log("골드가 부족합니다.");
                    return;
                }
                if(playerData.upgrade[index] >= upgrade[index]) {
                    Debug.Log("최대 레벨입니다.");
                    return;
                }
                Debug.Log(index);
                Debug.Log(index + "번째 업그레이드 클릭 : " + playerData.upgrade[index]);
                playerData.gold -= (int)(10 * Math.Pow(2, playerData.upgrade[index]));
                SetGoldText();
                upgradeSlot[index].transform.GetChild(playerData.upgrade[index]).GetChild(0).gameObject.SetActive(true);
                playerData.upgrade[index]++;
                SetReqGold(index, (int)(10 * Math.Pow(2, playerData.upgrade[index])));
                DataManager.Instance.SavePlayerData();
            });
        }
    }

    private void SetSlot() {
        for(int i = 0; i < upgradeSlot.Length; i++) {
            for(int j = 0; j < playerData.upgrade[i]; j++) {
                upgradeSlot[i].transform.GetChild(j).GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    private void Reset() {
        for(int i = 0; i < upgradeSlot.Length; i++) {
            for(int j = 0; j < playerData.upgrade[i]; j++) {
                // 업그레이드 슬롯 초기화
                upgradeSlot[i].transform.GetChild(j).GetChild(0).gameObject.SetActive(false);
                // 골드 돌려받기
                playerData.gold += (int)(10 * Math.Pow(2, j));
            }
            // 업그레이드 취소
            playerData.upgrade[i] = 0;
            // 필요 골드 텍스트 초기화
            SetReqGold(i, 10);
        }
        SetGoldText();
        DataManager.Instance.SavePlayerData();
    }
    
    private void SetGoldText() {
        gold.text = playerData.gold.ToString();
    }

    private void SetReqGold(int index, int gold) {
        reqGold[index].text = gold.ToString() + " 골드";
    }
}
