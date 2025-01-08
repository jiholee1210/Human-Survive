using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] Button startBtn;
    [SerializeField] Button endBtn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startBtn.onClick.AddListener(() => {
            SceneSelector.Instance.LoadGame();
        });
        endBtn.onClick.AddListener(() => {
            SceneSelector.Instance.ExitGame();
        });
    }
}
