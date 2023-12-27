using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Enemy
{
    public class EnemyActiveCollider : Action
    {
        public EnemyDeath EnemyDeath;
        
        public override TaskStatus OnUpdate()
        {
            EnemyDeath.ColliderActive();
            return TaskStatus.Success;
        }
    }
}