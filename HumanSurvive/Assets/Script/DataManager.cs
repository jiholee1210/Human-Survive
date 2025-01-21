using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set;}
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    string path;
    public PlayerData playerData;

    private void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
        Init();
    }

    void Start()
    {
        
    }

    public void Init() {
        path = Path.Combine(Application.persistentDataPath, "playerdata.json");
        if(!File.Exists(path)) {
            playerData = new PlayerData();
            SavePlayerData();
            Debug.Log("데이터 새로 생성");
        }
        else {
            LoadPlayerData();
        }
    }

    public void SavePlayerData() {
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(path, json);
        Debug.Log("데이터 저장");
    }

    public void LoadPlayerData() {
        string json = File.ReadAllText(path);
        playerData = JsonUtility.FromJson<PlayerData>(json);
        Debug.Log("데이터 로드");
    }
}

[System.Serializable]
public class PlayerData {
    public int gold = 0;

    // 아티팩트 전용 스텟
    public float hp = 0f;
    public float genHp = 0f;
    public float speed = 0f;
    public float attackSpeed = 0f;
    public float range = 0f;
    public int attackCount = 1;
    public float expRate = 0f;
    public float goldRate = 0f;
    public float finalDamage = 0f;

    // 무기 기본 스펙 업그레이드 및 기본 플레이어 스텟 업그레이드
    public int[] upgrade = {0, 0, 0, 0, 0, 0, 0, 0};

    public PlayerData Clone() {
        return new PlayerData {
            gold = this.gold,
            hp = this.hp,
            genHp = this.genHp,
            speed = this.speed,
            attackSpeed = this.attackSpeed,
            range = this.range,
            attackCount = this.attackCount,
            expRate = this.expRate,
            goldRate = this.goldRate,
            finalDamage = this.finalDamage,
            upgrade = this.upgrade,
        };
    }
}
