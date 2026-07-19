using UnityEngine;

public class CreatureViewAnimation : MonoBehaviour
{
    public Animator animator;

    public void EatObject()
    {
        animator.SetTrigger("EatObject");
    }
    public void Attack()
    {
        animator.SetTrigger("Attack");
    }
    public void IdleSpecial()
    {
        animator.SetTrigger("IdleScecial");
    }
}