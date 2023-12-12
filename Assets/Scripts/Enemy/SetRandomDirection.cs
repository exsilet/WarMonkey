using Behavior_Designer.Runtime.Variables;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Enemy
{
    public class SetRandomDirection : Action
    {
        public SharedVector2 Direction;
        public float CurrentDirection;

        public override TaskStatus OnUpdate()
        {
            Direction.Value = Random.insideUnitCircle.normalized * CurrentDirection;
            return TaskStatus.Success;
        }
    }
}