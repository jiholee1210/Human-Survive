using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange;
    public LayerMask targetLayer;
    public RaycastHit2D[] targets;
    public Transform nearTarget;

    private void FixedUpdate() {
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        nearTarget = GetNearTarget();
    }

    private Transform GetNearTarget() {
        Transform result = null;
        float diff = 100;

        foreach (RaycastHit2D target in targets) {
            Vector2 myPos = transform.position;
            Vector2 targetPos = target.transform.position;

            float curDiff = Vector2.Distance(myPos, targetPos);

            if(curDiff < diff) {
                diff = curDiff;
                result = target.transform;
            }
        }

        return result;
    }

}
