using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Enemy
{
    public class IsHealthStand : Conditional
    {
        public EnemyHealth EnemyHealth;

        public override TaskStatus OnUpdate()
        {
            if (EnemyHealth.Current <= 0)
                return TaskStatus.Success;
            else
                return TaskStatus.Failure;
        }
    }
}