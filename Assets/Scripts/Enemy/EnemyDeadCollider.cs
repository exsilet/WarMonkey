using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Enemy
{
    public class EnemyDeadCollider : Action
    {
        public EnemyDeath EnemyDeath;

        public override TaskStatus OnUpdate()
        {
            EnemyDeath.ColliderEnable();
            return TaskStatus.Success;
        }
    }
}