using System;
using Logic;
using UnityEngine;

namespace Player
{
    public class HeroHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private HeroAnimator _animator;
        [SerializeField] private AudioSource _soundHit;

        public event Action HealthChanged;

        public float Current { get; set; }
        public float Max { get; set; }

        public void TakeDamage(int damage)
        {
            Current -= damage;
            
            _animator.PlayHit();
            _soundHit.Play();
            
            HealthChanged?.Invoke();
        }
    }
}
