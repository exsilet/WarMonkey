using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Weapons
{
    public class ShootUI : MonoBehaviour
    {
        [SerializeField] private Slider _powerAttack;

        private float _currentPowerAttack;
        private bool _canShoot = false;
        private float _maxPowerAttack = 1f;
        private float _speedFilling = 1.5f;
        private bool _maxPower = true;
        private int _powerBullet = 5;

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
                
                if (_currentPowerAttack != _maxPowerAttack)
                {
                    _currentPowerAttack += Time.deltaTime * _speedFilling;
                    _powerAttack.value = _currentPowerAttack;
                }
                else
                {
                    _powerAttack.value = _powerAttack.maxValue;
                    _currentPowerAttack = _powerAttack.maxValue;
                    _maxPower = false;
                }
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