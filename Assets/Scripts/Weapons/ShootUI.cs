using UnityEngine;
using UnityEngine.UI;

namespace Weapons
{
    public class ShootUI : MonoBehaviour
    {
        [SerializeField] private Slider _powerAttack;

        private float _currentPowerAttack;
        private bool _canShoot;
        private float _maxPowerAttack = 1f;

        public float PowerAttack => _currentPowerAttack;

        private void Start()
        {
            _powerAttack.value = 0;
            _powerAttack.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (_canShoot)
            {
                _powerAttack.gameObject.SetActive(true);
                
                if (_currentPowerAttack != _powerAttack.maxValue)
                {
                    _currentPowerAttack += Time.deltaTime;
                }
                
                _powerAttack.value = _currentPowerAttack == _maxPowerAttack ? _powerAttack.maxValue : _currentPowerAttack;

            }
            else
            {
                _powerAttack.gameObject.SetActive(false);
                _currentPowerAttack = 0;
                _powerAttack.value = _currentPowerAttack;
            }
        }

        public void CanSHoot(bool canShoot)
        {
            _canShoot = canShoot;
        }
    }
}