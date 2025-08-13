using System;
using UnityEngine;

namespace Assets.Scripts.Movement
{
    public class MovementMediator : MonoBehaviour
    {
        public event Action<Vector3> OnMovementStarted;
        public event Action OnMovementStopped;

        public event EventHandler<EffectiveAreaEventArgs> OnEnteredEffectiveArea;
        public event EventHandler<EffectiveAreaEventArgs> OnExitedEffectiveArea;

        public void TriggerMovementStart(Vector3 movementDirection) => OnMovementStarted?.Invoke(movementDirection);
        public void TriggerMovementStop() => OnMovementStopped?.Invoke();

        public void TriggerEnteredEffectiveArea(string feedback, AudioClip audioClip) => OnEnteredEffectiveArea?.Invoke(this, new EffectiveAreaEventArgs(feedback, audioClip));
        public void TriggerExitedEffectiveArea(string feedback, AudioClip audioClip) => OnExitedEffectiveArea?.Invoke(this, new EffectiveAreaEventArgs(feedback, audioClip));
    }
}