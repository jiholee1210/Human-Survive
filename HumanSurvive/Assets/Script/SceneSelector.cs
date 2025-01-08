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
            Destroy(gameObject);
        }
    }

    private void Update() {
    }

    public void LoadMain() {
        SceneManager.LoadScene("New Scene");
    }

    public void LoadGame() {
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitGame() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
