using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _inputSourceBehaviour;
        [SerializeField] private Transform _startPosition;

        public float _speed;
        public CharacterController _characterController;
        
        private IInputBotService _inputService;
        private float Epsilon = 0.001f;
        private Vector3 _currentMovement;
        private bool _isMovedPressed;
        private NavMeshAgent _agent;
        private const float _minimalDistance = 0;

        private void Start()
        {
            //_agent.destination = _startPosition.position;
        }

        private void Awake()
        {
            _inputService = (IInputBotService)_inputSourceBehaviour;
            _agent = GetComponent<NavMeshAgent>();
            //_inputService = AllServices.Container.Single<IInputService>();
        }
        
        private void Update()
        {
            //MovementInput();
            MovementDirection();
        }

        private void MovementDirection()
        {
            var movement = new Vector3(_inputService.MoveInput.x, 0f, _inputService.MoveInput.y);
            movement *= _speed;
            _agent.SetDestination(movement);
        }
        
        private void MovementInput()
        {
            var movement = new Vector3(_inputService.MoveInput.x, 0f, _inputService.MoveInput.y);
            movement *= _speed;
            _characterController.SimpleMove(movement);
        }
    }
}