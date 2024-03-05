using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    [RequireComponent(typeof(EnemyHealth), typeof(EnemyAnimator))]
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private CapsuleCollider _collider;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private NavMeshAgent _agent;

        public bool Death = false;
        
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
            Death = true;
            //_agent.gameObject.SetActive(false);
            _rigidbody.isKinematic = true;
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(2);
            Destroy(gameObject);
        }
    }
}