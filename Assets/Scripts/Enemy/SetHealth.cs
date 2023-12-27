using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace Enemy
{
    public class SetHealth : Action
    {
        public SharedInt Health;
        public EnemyHealth EnemyHealth;
        
        public override TaskStatus OnUpdate()
        {
            EnemyHealth.Current = Health.Value;
            return TaskStatus.Success;
        }
    }
}