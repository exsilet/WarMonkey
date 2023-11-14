using UnityEngine;
using Weapons;

namespace Enemy
{
    public class DestroyObjects : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Bullet bullet))
            {
                Destroy(bullet);
            }
        }
    }
}