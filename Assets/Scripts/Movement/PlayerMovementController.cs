using UnityEngine;

namespace Assets.Scripts.Movement
{
    public class PlayerMovementController : MonoBehaviour
    {
        public MovementButton currentMovementButton;
        public MovementMediator movementMediator;

        [SerializeField] private Rigidbody rigidbody;

        private Vector3 moveDirection;

        private float moveSpeed = 4f;

        [Header("Clamp Settings")]
        [SerializeField] private float maxX;
        [SerializeField] private float maxZ;
        [SerializeField] private float minX;
        [SerializeField] private float minZ;

        public bool isMoving;

        void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            movementMediator = FindAnyObjectByType<MovementMediator>();
        }

        void Update()
        {
            if (!isMoving)
            {
                return;
            }
            if (currentMovementButton.isHorizontalMovementButton)
            {
                MoveHorizontal(moveDirection);
            }
            else
            {
                MoveVertical(moveDirection);
            }
        }

        public void MoveHorizontal(Vector3 moveDirection)
        {
            this.moveDirection = currentMovementButton.moveDirection;
            moveDirection = this.moveDirection;
            Vector3 newPosition = rigidbody.position + moveDirection * (moveSpeed * Time.deltaTime);
            rigidbody.MovePosition(newPosition);
            ClampPosition();
        }

        public void MoveVertical(Vector3 moveDirection)
        {
            this.moveDirection = currentMovementButton.moveDirection;
            moveDirection = this.moveDirection;
            Vector3 newPosition = rigidbody.position + moveDirection * (moveSpeed * Time.deltaTime);
            rigidbody.MovePosition(newPosition);
            ClampPosition();
        }

        private void ClampPosition()
        {
            Vector3 position = transform.position;
            position.x = Mathf.Clamp(position.x, minX, maxX);
            position.z = Mathf.Clamp(position.z, minZ, maxZ);
            transform.position = position;
        }

        void OnEnable()
        {
            movementMediator.OnMovementStarted += MoveHorizontal;
            movementMediator.OnMovementStarted += MoveVertical;

        }
        void OnDisable()
        {
            movementMediator.OnMovementStarted -= MoveHorizontal;
            movementMediator.OnMovementStarted -= MoveVertical;
        }
    }
}