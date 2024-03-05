using Enemy;
using UnityEngine;

namespace Weapons
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private int _damage;
        [SerializeField] private GameObject _hitEffect;

        private float _timeDestroy = 0.5f;

        private void Update()
        {
            transform.Translate(transform.forward * _speed * Time.deltaTime, Space.World);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EnemyHealth enemy))
            {
                enemy.TakeDamage(_damage);
                Instantiate(_hitEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            
            Destroy(gameObject, _timeDestroy);
        }
    }
}