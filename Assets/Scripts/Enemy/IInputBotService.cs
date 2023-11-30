using UnityEngine;

namespace Enemy
{
    public interface IInputBotService
    {
        Vector2 MoveInput { get; }
    }
}