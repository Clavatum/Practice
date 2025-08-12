using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Movement
{
    public class MovementButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private MovementMediator movementMediator;

        [SerializeField] private Vector3 moveDirection;

        void Awake()
        {
            movementMediator = FindAnyObjectByType<MovementMediator>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            movementMediator.TriggerMovementStart(moveDirection);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            movementMediator.TriggerMovementStop();
        }
    }
}