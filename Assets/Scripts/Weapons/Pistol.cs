using UnityEngine;

namespace Weapons
{
    public class Pistol : Weapon
    {
        public override void Shoot(Transform shootPoint, float power)
        {
            foreach (var bullet in Bullets)
            {
                CreateBullet(shootPoint, power <= 0.7f ? Bullets[0] : Bullets[1]);
                break;
            }
        }

        private static void CreateBullet(Transform shootPoint, Bullet bullet)
        {
            Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        }
    }
}