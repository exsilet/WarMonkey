using UnityEngine;

namespace Weapons
{
    public class ThrowingWeapon : EnemyWeapon
    {
        public override void Shoot(Transform shootPoint)
        {
            Instantiate(Bullet, shootPoint.position, shootPoint.rotation);
        }
    }
}