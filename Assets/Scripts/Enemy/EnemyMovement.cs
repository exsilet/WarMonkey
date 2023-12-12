using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _inputSourceBehaviour;
        [SerializeField] private Transform _startPosition;

        public float Speed;
        private IInputBotService _inputService;
        private float Epsilon = 0.001f;
        private Vector3 _currentMovement;
        private bool _isMovedPressed = true;
        private NavMeshAgent _agent;
        private const float _minimalDistance = 0;

        private void Awake()
        {
            _inputService = (IInputBotService)_inputSourceBehaviour;
            _agent = GetComponent<NavMeshAgent>();
        }
        
        private void Update()
        {
            if (_isMovedPressed)
                StartMoving();
            else
                MovementDirection();
        }

        private void MovementDirection()
        {
            var movement = new Vector3(_inputService.MoveInput.x, 0f, _inputService.MoveInput.y);
            movement *= Speed;
            _agent.SetDestination(movement);
        }
        
        private void StartMoving()
        {
            var movement = new Vector3(_startPosition.position.x, 0f, _startPosition.position.y);
            movement *= Speed;
            _agent.SetDestination(movement);
        }
        
        // private void MovementInput()
        // {
        //     var movement = new Vector3(_inputService.MoveInput.x, 0f, _inputService.MoveInput.y);
        //     movement *= Speed;
        //     _characterController.SimpleMove(movement);
        // }
    }
}