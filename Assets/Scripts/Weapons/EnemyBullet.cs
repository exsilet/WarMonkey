using Player;
using UnityEngine;

namespace Weapons
{
    public class EnemyBullet : MonoBehaviour
    {
        [SerializeField] private float _shootingForce;
        [SerializeField] private int _damage;
        [SerializeField] private GameObject _hitEffect;

        private float _timeDestroy = 0.5f;
        
        private void Update()
        {
            transform.Translate(transform.forward * _shootingForce * Time.deltaTime, Space.World);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out HeroHealth hero))
            {
                hero.TakeDamage(_damage);
                Instantiate(_hitEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            
            Destroy(gameObject, _timeDestroy);
        }
    }
}