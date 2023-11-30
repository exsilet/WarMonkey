using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using Weapons;

namespace Enemy
{
    public class SetShoot : Action
    {
        public EnemyWeapon Weapon;
        public Transform ShootPoint;

        public override TaskStatus OnUpdate()
        {
            Weapon.Shoot(ShootPoint);
            return TaskStatus.Success;
        }
    }
}