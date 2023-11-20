using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private int _speed;
        [SerializeField] private Transform _path;
        [SerializeField] private Transform _startTargetPoint;

        private bool _startGame;
        private int _transformRotation = 100;
        private Transform[] _point;
        private int _currentPoint;
        private Vector3 _currentMovement;

        public Transform StartPlay => _startTargetPoint;

        private void Start()
        {
            MovementToGame();
            //transform.rotation = new Quaternion(0, _transformRotation, 0, 0);
        }

        private void Update()
        {
            MovingStart();
            //Movement();
        }

        private void MovementToGame()
        {
            _point = new Transform[_path.childCount];

            for (int i = 0; i < _path.childCount; i++)
            {
                _point[i] = _path.GetChild(i);
            }
        }

        private void MovingStart()
        {
            if (transform.position != _startTargetPoint.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, _startTargetPoint.position, _speed * Time.deltaTime);
                _startGame = false;
            }
            else
            {
                MovingAroundTheSite();
            }
        }

        private void MovingAroundTheSite()
        {
            StartCoroutine(SwapPosition());
            
            Transform target = _point[_currentPoint];
            transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
        }

        private IEnumerator SwapPosition()
        {
            yield return new WaitForSeconds(5f);
            
            if (transform.position == _startTargetPoint.position)
            {
                _currentPoint = Random.Range(0, _point.Length);
                
                if (_currentPoint >= _point.Length)
                {
                    _currentPoint = 0;
                }
            }
        }
    }
}