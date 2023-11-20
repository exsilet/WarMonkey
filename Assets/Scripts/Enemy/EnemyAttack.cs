using UnityEngine;
using Weapons;

namespace Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private EnemyAnimator _enemyAnimator;

        private bool _stand;

        public void Attack()
        {
            if (_stand == false)
            {
                _enemyAnimator.PlayHit();
            }
        }
        
        public void OnAttack()
        {
            _weapon.Shoot(_shootPoint, 1);
        }
    }
}