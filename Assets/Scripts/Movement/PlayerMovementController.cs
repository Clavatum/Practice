using UnityEngine;

namespace Assets.Scripts.Movement
{
    public class PlayerMovementController : MonoBehaviour
    {
        private MovementMediator movementMediator;

        private Rigidbody rigidbody;

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
            if (!isMoving) return;

            Vector3 newPosition = rigidbody.position + moveDirection * (moveSpeed * Time.deltaTime);
            rigidbody.MovePosition(newPosition);
            ClampPosition();
        }

        private void StartMovement(Vector3 direction)
        {
            moveDirection = direction;
            isMoving = true;
        }

        private void StopMovement()
        {
            isMoving = false;
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
            movementMediator.OnMovementStarted += StartMovement;
            movementMediator.OnMovementStopped += StopMovement;
        }

        void OnDisable()
        {
            movementMediator.OnMovementStarted -= StartMovement;
            movementMediator.OnMovementStopped -= StopMovement;
        }
    }
}