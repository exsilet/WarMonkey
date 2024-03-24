using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    [RequireComponent(typeof(HeroHealth), typeof(HeroAnimator))]
    public class HeroDeath : MonoBehaviour
    {
        [SerializeField] private HeroHealth _health;
        [SerializeField] private HeroAnimator _animator;
        [SerializeField] private AudioSource _soundDeath;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _collider;

        private int _playerKilled;
        
        public UnityAction<int> SlainPlayer;
        
        private void Start()
        {
            _health.HealthChanged += OnHealthChanged;
        }

        private void OnDestroy()
        {
            _health.HealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            if (_health.Current <= 0)
                Die();
        }

        private void Die()
        {
            //_health.HealthChanged -= OnHealthChanged;
      
            _animator.PlayDeath();
            _playerKilled++;
            SlainPlayer?.Invoke(_playerKilled);
            _soundDeath.Play();
            _rigidbody.isKinematic = true;
            _collider.enabled = false;
            //StartCoroutine(DestroyTimer());
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(3);
            Destroy(gameObject);
        }
    }
}