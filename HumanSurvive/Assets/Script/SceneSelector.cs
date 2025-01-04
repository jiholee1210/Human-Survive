using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelector : MonoBehaviour
{
    public static SceneSelector Instance { get; private set;}
    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            DestroyObject(gameObject);
        }
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene("SampleScene");
        }
        if(Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene("New Scene");
        }
    }
}
