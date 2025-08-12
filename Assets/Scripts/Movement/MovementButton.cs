using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Movement
{
    public class MovementButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private MovementMediator movementMediator;
        private PlayerMovementController playerMovementController;

        public bool isHorizontalMovementButton;
        public bool isMoveLeftButton;
        public bool isMoveUpButton;
        public Vector3 moveDirection;

        void Awake()
        {
            playerMovementController = FindAnyObjectByType<PlayerMovementController>();
            movementMediator = FindAnyObjectByType<MovementMediator>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            playerMovementController.currentMovementButton = this;
            playerMovementController.isMoving = true;
            movementMediator.TriggerMovementStart(moveDirection);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            playerMovementController.isMoving = false;
            movementMediator.TriggerMovementStop();
        }
    }
}