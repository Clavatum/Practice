using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Movement
{
    public class MovementButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private Mediator mediator;

        [SerializeField] private Vector3 moveDirection;

        void Awake()
        {
            mediator = FindAnyObjectByType<Mediator>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            mediator.TriggerMovementStart(moveDirection);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            mediator.TriggerMovementStop();
        }
    }
}