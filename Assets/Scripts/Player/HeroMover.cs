using Infrastructure.Service;
using UnityEngine;

namespace Player
{
    public class HeroMover : MonoBehaviour
    {
        [SerializeField] private float _speed;
        //[SerializeField] private CharacterController _characterController;
        [SerializeField] private HeroAnimator _animator;
        [SerializeField] private Rigidbody _rigidbody;

        private Selectable _selectable;
        private IInputService _inputService;
        private float Epsilon = 0.001f;
        private Vector3 _currentMovement;
        private bool _isMovedPressed;
        private Camera _camera;

        public float Speed
        {
            get => _speed;
            set => _speed = value;
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Awake()
        {
            //_characterController = GetComponent<CharacterController>();
            _animator = GetComponent<HeroAnimator>();
            _selectable = GetComponent<Selectable>();
            _inputService = AllServices.Container.Single<IInputService>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (_selectable.Selected)
            {
                //OnMovementInput();
            }
        }
        
        // private void OnMovementInput()
        // {
        //     _currentMovement = Vector3.zero;
        //
        //     if (_inputService.Axis.sqrMagnitude > Epsilon)
        //     {
        //         _currentMovement = _camera.transform.TransformDirection(_inputService.Axis);
        //
        //         //_currentMovement.y = 0;
        //         //_currentMovement.Normalize();
        //
        //         //transform.forward = _currentMovement;
        //     }
        //
        //     HandleGravity();
        //     
        //     _characterController.Move(_currentMovement * _speed * Time.deltaTime);
        // }
        //
        // private void HandleGravity()
        // {
        //     if (_characterController.isGrounded)
        //     {
        //         float groundedGravity = -.05f;
        //         _currentMovement.y = groundedGravity;
        //     }
        //     else
        //     {
        //         float gravity = -9.8f;
        //         _currentMovement.y = gravity;
        //     }
        // }
    }
}