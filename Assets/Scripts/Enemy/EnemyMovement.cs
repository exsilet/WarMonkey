using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _inputSourceBehaviour;

        public float Speed;
        private IInputBotService _inputService;
        private float Epsilon = 0.001f;
        private NavMeshAgent _agent;

        private void Awake()
        {
            _inputService = (IInputBotService)_inputSourceBehaviour;
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            MovementDirection();
        }

        private void MovementDirection()
        {
            var movement = new Vector3(_inputService.MoveInput.x, 0f, _inputService.MoveInput.y);
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