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
    }

    void Start()
    {
        path = Path.Combine(Application.persistentDataPath, "playerdata.json");
        if(!File.Exists(path)) {
            playerData = new PlayerData();
            SavePlayerData();
        }
        else {
            LoadPlayerData();
        }
    }

    public void SavePlayerData() {
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(path, json);
    }

    public void LoadPlayerData() {
        string json = File.ReadAllText(path);
        playerData = JsonUtility.FromJson<PlayerData>(json);
    }
}

[System.Serializable]
public class PlayerData {
    public int gold = 0;
    public float hp = 1f;
    public float genHp = 1f;
    public float speed = 1f;
    public float finalDamage = 1f;
    public int[] upgrade = {0, 0, 0, 0, 0, 0, 0, 0};

    public PlayerData Clone() {
        return new PlayerData {
            gold = this.gold,
            hp = this.hp,
            genHp = this.genHp,
            speed = this.speed,
            finalDamage = this.finalDamage,
            upgrade = this.upgrade,
        };
    }
}
