using Infrastructure.Service;
using UnityEngine;
using Weapons;

namespace Player
{
    public class HeroAttack : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private HeroAnimator _heroAnimator;

        private Selectable _selectable;
        private IInputService _input;
        private bool _canShoot;
        private ShootUI _shootUI;

        private void Awake()
        {
            _input = AllServices.Container.Single<IInputService>();
            _selectable = GetComponent<Selectable>();
            _shootUI = GetComponent<ShootUI>();
        }

        private void Update()
        {
            if (_selectable.Selected)
            {
                if (_input.IsAttackButtonUp())
                {
                    _heroAnimator.PlayAttack();
                    _canShoot = true;
                    _shootUI.CanSHoot(_canShoot);
                }
            
                if (Input.GetMouseButtonUp(0))
                {
                    _heroAnimator.PlayAttackButtonUp();
                    _canShoot = false;
                    _shootUI.CanSHoot(_canShoot);
                    OnAttack();
                }
            }
        }

        private void OnAttack()
        {
            _weapon.Shoot(_shootPoint, _shootUI.PowerAttack);
        }

        // private IEnumerator AttackPower()
        // {
        //     // foreach (var VARIABLE in COLLECTION)
        //     // {
        //     //     
        //     // }
        //     // yield return new WaitForSeconds(1f);
        // }
    }
}