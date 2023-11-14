using System;
using Data;
using Logic;
using UnityEngine;

namespace Player
{
    public class HeroHealth : MonoBehaviour, IHealth
    {
        private State _state;
        private HeroAnimator _animator;
        
        public event Action HealthChanged;

        public float Current
        {
            get => _state.CurrentHP;
            set
            {
                if ((int)value != _state.CurrentHP)
                {
                    _state.CurrentHP = (int)value;
          
                    HealthChanged?.Invoke();
                }
            }
        }

        public float Max
        {
            get => _state.MaxHP;
            set => _state.MaxHP = (int)value;
        }
        
        public void TakeDamage(int damage)
        {
            if (Current <= 0)
                return;
            
            Current -= damage;
            _animator.PlayHit();
        }
    }
}
