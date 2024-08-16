using System;
using UnityEngine;

public class CharacterVisualController : MonoBehaviour
{
    public event Action OnAttackAnimationFinished;

    private Animator animator;
    private ParticleSystem runningParticleSystem;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        runningParticleSystem = GetComponentInChildren<ParticleSystem>(); // Asigna autom�ticamente el sistema de part�culas si est� en un hijo
    }

    private void Update()
    {
        // Obt�n el estado del par�metro IsRunning y actualiza el sistema de part�culas.
        bool isRunning = animator.GetBool("IsRunning");
        UpdateParticleSystem(isRunning);
    }

    public void SetBoolAnimation(string animationParamName, bool parameterState)
    {
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

    private void UpdateParticleSystem(bool isRunning)
    {
        if (runningParticleSystem != null)
        {
            if (isRunning && !runningParticleSystem.isPlaying)
            {
                runningParticleSystem.Play(); // Activa el sistema de part�culas
                Debug.Log("Particle System Started");
            }
            else if (!isRunning && runningParticleSystem.isPlaying)
            {
                runningParticleSystem.Stop(); // Desactiva el sistema de part�culas
                Debug.Log("Particle System Stopped");
            }
        }
        else
        {
            Debug.LogWarning("Running Particle System not assigned.");
        }
    }
}
