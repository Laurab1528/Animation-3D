using System;
using UnityEngine;

public class CharacterVisualController : MonoBehaviour
{
    public event Action OnAttackAnimationFinished;
    
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetBoolAnimation(string animationParamName, bool parameterState)
    {
        if(animator.GetBool(animationParamName) != parameterState)
            animator.SetBool(animationParamName, parameterState);
    }

    public void SetTriggerAnimation(string animationParamName)
    {
        animator.SetTrigger(animationParamName);
    }

    public void RaiseFinishAttackAnimation()
    {
        OnAttackAnimationFinished?.Invoke();
    }
}
