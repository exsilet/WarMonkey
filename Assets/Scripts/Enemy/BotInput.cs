using UnityEngine;

namespace Enemy
{
    public class BotInput : MonoBehaviour, IInputBotService
    {
        public Vector2 MoveInput { get; set; }
    }
}