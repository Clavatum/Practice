using System;
using UnityEngine;

namespace Assets.Scripts.Movement
{
    public class MovementMediator : MonoBehaviour
    {
        public event Action<Vector3> OnMovementStarted;
        public event Action OnMovementStopped;

        public void TriggerMovementStart(Vector3 movementDirection) => OnMovementStarted?.Invoke(movementDirection);

        public void TriggerMovementStop() => OnMovementStopped?.Invoke();
    }
}