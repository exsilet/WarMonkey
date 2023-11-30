using Behavior_Designer.Runtime.Variables;
using BehaviorDesigner.Runtime.Tasks;

namespace Enemy
{
    public class SetMovementInput : Action
    {
        public SharedBotInput SelfBotInput;
        public SharedVector2 Direction;

        public override TaskStatus OnUpdate()
        {
            SelfBotInput.Value.MoveInput = Direction.Value;
            return TaskStatus.Success;
        }
    }
}