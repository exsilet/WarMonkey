using System;
using Data;
using Infrastructure.Service.SaveLoad;
using Logic;
using UnityEngine;

namespace Player
{
    public class HeroHealth : MonoBehaviour, IHealth, ISavedProgress
    {
        [SerializeField] private HeroAnimator _animator;
        
        private State _state;
        
        public event Action HealthChanged;

        public float Current
        {
            get => _state.CurrentHP;
            set
            {
                if (value != _state.CurrentHP)
                {
                    _state.CurrentHP = value;
          
                    HealthChanged?.Invoke();
                }
            }
        }

        public float Max
        {
            get => _state.MaxHP;
            set => _state.MaxHP = (int)value;
        }
        
        public void LoadProgress(PlayerProgress progress)
        {
            _state = progress.HeroState;
            HealthChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.HeroState.CurrentHP = Current;
            progress.HeroState.MaxHP = Max;
        }
        
        public void TakeDamage(int damage)
        {
            Current -= damage;
            
            _animator.PlayHit();
            
            HealthChanged?.Invoke();
        }
        

    }
}
