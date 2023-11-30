using System.Collections;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(HeroHealth), typeof(HeroAnimator))]
    public class HeroDeath : MonoBehaviour
    {
        [SerializeField] private HeroHealth _health;
        [SerializeField] private HeroAnimator _animator;

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
            _health.HealthChanged -= OnHealthChanged;
      
            _animator.PlayDeath();

            StartCoroutine(DestroyTimer());
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(3);
            Destroy(gameObject);
        }
    }
}