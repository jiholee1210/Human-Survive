using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    public GameObject player;
    
    private void Awake() {
        Instance = this;
    }
}
