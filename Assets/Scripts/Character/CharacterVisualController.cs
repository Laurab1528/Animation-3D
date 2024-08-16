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
        runningParticleSystem = GetComponentInChildren<ParticleSystem>(); // Asigna automáticamente el sistema de partículas si está en un hijo
    }

    private void Update()
    {
        // Obtén el estado del parámetro IsRunning y actualiza el sistema de partículas.
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
                runningParticleSystem.Play(); // Activa el sistema de partículas
                Debug.Log("Particle System Started");
            }
            else if (!isRunning && runningParticleSystem.isPlaying)
            {
                runningParticleSystem.Stop(); // Desactiva el sistema de partículas
                Debug.Log("Particle System Stopped");
            }
        }
        else
        {
            Debug.LogWarning("Running Particle System not assigned.");
        }
    }
}
