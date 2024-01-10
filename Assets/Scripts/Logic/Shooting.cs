using Player;
using UnityEngine;
using UnityEngine.UI;

namespace Logic
{
    public class Shooting : MonoBehaviour
    {
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _shootSpawnPoint;
        [SerializeField] private Slider _forceSlider;
        [SerializeField] private HeroAnimator _heroAnimator;
        [SerializeField] private float _maxForce;
        [SerializeField] private float _maxHoldTime = 2f;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _clipFlight;

        private float _speedFilling = 3f;
        private float currentForce = 0f;
        private float currentHoldTime = 0f;
        private bool charging = false;

        private void Update()
        {
            UpdateForce();
        }

        public void StartCharging()
        {
            _heroAnimator.PlayAttack();
            currentForce = 0f;
            currentHoldTime = 0f;
            charging = true;
        }

        public void StopCharging()
        {
            if (charging)
            {
                _heroAnimator.PlayAttackButtonUp();
                Shoot();
                charging = false;
                _forceSlider.gameObject.SetActive(false);
            }
        }

        private void UpdateForce()
        {
            if (charging)
            {
                _forceSlider.gameObject.SetActive(true);
                currentHoldTime += Time.deltaTime;
                float holdPercentage = currentHoldTime / _maxHoldTime;
                _forceSlider.value = holdPercentage * _speedFilling;

                currentForce = Mathf.Lerp(0f, _maxForce, holdPercentage);
            }
        }

        private void Shoot()
        {
            GameObject projectile = Instantiate(_bulletPrefab, _shootSpawnPoint.position,
                _shootSpawnPoint.rotation);
            Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
            _audioSource.PlayOneShot(_clipFlight);

            projectileRigidbody.AddForce(_shootSpawnPoint.forward * currentForce, ForceMode.Impulse);
        }
    }
}