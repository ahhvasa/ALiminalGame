using UnityEngine;

public class SpriteFlipByTarget : MonoBehaviour
{
    [SerializeField] private Transform spriteRenderer;
    [SerializeField] private Transform lookTarget;

    private void Update()
    {
        if (lookTarget == null || spriteRenderer == null)
            return;

        Vector3 toTarget = (lookTarget.position - transform.position).normalized;

        float angle = Vector3.SignedAngle(
            transform.forward,
            toTarget,
            Vector3.up);

        //spriteRenderer.flipX = angle > 0f;
        spriteRenderer.transform.localScale =  new Vector3(angle > 0f ? -1 : 1,1,1);
    }
}