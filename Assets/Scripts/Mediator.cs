using System;
using UnityEngine;

public class Mediator : MonoBehaviour
{
    public event Action<Vector3> OnMovementStarted;
    public event Action OnMovementStopped;

    public event EventHandler<EffectiveAreaEventArgs> OnEnteredEffectiveArea;
    public event EventHandler<EffectiveAreaEventArgs> OnExitedEffectiveArea;

    public void TriggerMovementStart(Vector3 movementDirection) => OnMovementStarted?.Invoke(movementDirection);
    public void TriggerMovementStop() => OnMovementStopped?.Invoke();

    public void TriggerEnteredEffectiveArea(string feedback, AudioClip audioClip, Vector3 targetScale)
    {
        OnEnteredEffectiveArea?.Invoke(this, new EffectiveAreaEventArgs(feedback, audioClip, targetScale));
    }
    public void TriggerExitedEffectiveArea(string feedback, AudioClip audioClip, Vector3 targetScale)
    {
        OnExitedEffectiveArea?.Invoke(this, new EffectiveAreaEventArgs(feedback, audioClip, targetScale));
    }
}