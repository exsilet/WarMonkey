using Infrastructure.Service;
using UnityEngine;

namespace Player
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }
        bool IsAttackButtonUp();
    }
}