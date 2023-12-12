using System;
using Logic;
using UnityEngine;

namespace Player
{
    public class HeroHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private HeroAnimator _animator;

        public event Action HealthChanged;

        public float Current { get; set; }
        public float Max { get; set; }

        public void TakeDamage(int damage)
        {
            Current -= damage;
            
            _animator.PlayHit();
            
            HealthChanged?.Invoke();
        }
    }
}
