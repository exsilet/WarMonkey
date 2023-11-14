using System;
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

        private void Awake()
        {
            _input = AllServices.Container.Single<IInputService>();
            _selectable = GetComponent<Selectable>();
        }

        private void Update()
        {
            if (_selectable.Selected)
            {
                if (_input.IsAttackButtonUp())
                {
                    _heroAnimator.PlayAttack();
                }
            
                if (Input.GetMouseButtonUp(0))
                {
                    _heroAnimator.PlayAttackButtonUp();
                }
            }
        }

        public void OnAttack()
        {
            _weapon.Shoot(_shootPoint, 1);
        }
    }
}