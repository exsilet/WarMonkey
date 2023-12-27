using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace Enemy
{
    public class IsHealthUnder : Action
    {
        public SharedInt HealthTreshold;
        public EnemyHealth EnemyHealth;

        public override TaskStatus OnUpdate()
        {
            return EnemyHealth.Current < HealthTreshold.Value ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}