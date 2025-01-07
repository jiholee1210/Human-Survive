using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int level;
    public float curExp;
    public float maxExp;

    private void Start() {

    }

    public int GetExp(float exp) {
        curExp += exp;
        if(curExp >= maxExp) {
            curExp -= maxExp;
            LevelUp();
            return 1;
        }
        return 0;
    }

    private void LevelUp() {
        level++;
        maxExp *= 2;
    }

    public void Init() {
        level = 1;
        curExp = 0;
        maxExp = 10;
    }

    public void Die() {
        GameManager.Instance.GameEnd();
    }
}
