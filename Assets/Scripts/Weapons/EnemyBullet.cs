using Player;
using UnityEngine;

namespace Weapons
{
    public class EnemyBullet : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private int _damage;

        private void Update()
        {
            transform.Translate(transform.forward * _speed * Time.deltaTime, Space.World);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out HeroHealth hero))
            {
                hero.TakeDamage(_damage);
                Destroy(gameObject);
            }
        }
    }
}