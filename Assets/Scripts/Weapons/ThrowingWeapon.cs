using UnityEngine;

namespace Weapons
{
    public class ThrowingWeapon : EnemyWeapon
    {
        [SerializeField] private AudioSource _soundShoot;
        
        public override void Shoot(Transform shootPoint)
        {
            Instantiate(Bullet, shootPoint.position, shootPoint.rotation);
            _soundShoot.Play();
        }
    }
}