using System.Collections;
using UI.Element;
using UnityEngine;

namespace Enemy
{
    public class EnemyMonkey : MonoBehaviour
    {
        [SerializeField] private int _speed;
        [SerializeField] private Transform _startTargetPoint;
        [SerializeField] private float _timeStopAttack;

        private bool _startGame;
        private StartBattle _startBattle;
        public bool Shoot;

        private void Update()
        {
            if (!_startBattle.CurrentStartBattle)
                MovementToGame();
            else if (!Shoot)
            {
                StartCoroutine(StopToAttack());
            }
            else
            {
                Shoot = false;
            }
        }

        public void Construct(StartBattle startBattle)
        {
            _startBattle = startBattle;
        }

        public void MoveTo(Vector3 targetPosition)
        {
            targetPosition.y = transform.position.y;

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
        }

        private void MovementToGame()
        {
            transform.position =
                Vector3.MoveTowards(transform.position, _startTargetPoint.position, _speed * Time.deltaTime);
        }

        private IEnumerator StopToAttack()
        {
            yield return new WaitForSeconds(_timeStopAttack);
            Shoot = true;
        }
    }
}