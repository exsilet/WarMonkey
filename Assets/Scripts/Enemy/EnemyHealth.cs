using System;
using Logic;
using UnityEngine;

namespace Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private float _current;
        [SerializeField] private float _max;
        [SerializeField] private EnemyAnimator _animator;

        public event Action HealthChanged;

        public float Current
        {
            get => _current;
            set => _current = value;
        }

        public float Max
        {
            get => _max;
            set => _max = value;
        }

        public void TakeDamage(int damage)
        {
            Current -= damage;
      
            _animator.PlayHit();
      
            HealthChanged?.Invoke();
        }
    }
}