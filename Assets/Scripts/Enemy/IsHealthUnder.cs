using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace Enemy
{
    public class IsHealthUnder : Conditional
    {
        public SharedInt HealthTreshold;
        public EnemyHealth EnemyHealth;

        public override TaskStatus OnUpdate()
        {
            if (EnemyHealth.Current < HealthTreshold.Value)
                return TaskStatus.Success;
            else
                return TaskStatus.Failure;
        }
    }
}