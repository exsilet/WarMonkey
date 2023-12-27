using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace Enemy
{
    public class GotoNextStage : Action
    {
        public SharedInt CurrentStage;

        public override TaskStatus OnUpdate()
        {
            return base.OnUpdate();
        }
    }
}