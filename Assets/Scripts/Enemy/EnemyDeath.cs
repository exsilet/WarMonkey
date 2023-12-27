using System;
using System.Collections;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(EnemyHealth), typeof(EnemyAnimator))]
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private CapsuleCollider _collider;

        public event Action Happened;

        private void Start()
        {
            _health.HealthChanged += OnHealthChanged;
        }

        private void OnDestroy()
        {
            _health.HealthChanged -= OnHealthChanged;
        }

        public void ColliderEnable()
        {
            _collider.enabled = false;
        }

        public void ColliderActive()
        {
            _collider.enabled = true;
        }

        private void OnHealthChanged()
        {
            if (_health.Current <= 0)
                Die();
        }

        private void Die()
        {
            Happened?.Invoke();
            
            //StartCoroutine(DestroyTimer());
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(2);
            Destroy(gameObject);
        }
    }
}