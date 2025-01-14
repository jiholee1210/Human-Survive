using System.Collections;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(WaitForAnimation(animator, "Damage_text"));
    }

    private IEnumerator WaitForAnimation(Animator animator, string animationStateName) {
        // 애니메이션이 끝날 때까지 대기
        while (true) {
            // 현재 애니메이션 상태 정보 가져오기
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            // 애니메이션이 끝났는지 확인
            if (stateInfo.IsName(animationStateName) && stateInfo.normalizedTime >= 1.0f) {
                break; // 애니메이션이 끝났으면 루프 종료
            }
            yield return null; // 다음 프레임까지 대기
        }
        Destroy(gameObject);
    }
}
