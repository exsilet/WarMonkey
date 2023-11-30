using UnityEngine;

namespace Weapons
{
    public abstract class EnemyWeapon : MonoBehaviour
    {
        [SerializeField] protected EnemyBullet Bullet;

        public abstract void Shoot(Transform shootPoint);
    }
}