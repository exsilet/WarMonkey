using System;
using UnityEngine;

namespace Player
{
    public class SelectUnit : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;

        public Selectable CurrentSelectable;
        public HeroMover CurrentSelectHero;
        public HeroMover Hero;
        
        private Camera _camera;
        
        private void Start()
        {
            _camera = Camera.main;
        }
        
        private void LateUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                SelectPart();
                Hero = CurrentSelectHero;
            }

            if (Input.GetMouseButtonUp(0))
            {
                //UpMouse();
            }
        }
        
        private void SelectPart()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(transform.position, transform.forward * 100f, Color.blue);

            if (Physics.Raycast(ray, out var hit))
            {
                Selectable selectable = hit.collider.gameObject.GetComponent<Selectable>();
                CurrentSelectHero = hit.collider.gameObject.GetComponent<HeroMover>();

                if (selectable)
                {
                    if (CurrentSelectable && CurrentSelectable != selectable)
                    {
                        Debug.Log("Unselect 1");
                        Hero.UnSelect();
                        selectable.Deselect();
                    }

                    CurrentSelectable = selectable;
                    selectable.Select();
                    CurrentSelectHero.OnSelect();
                    Debug.Log("select monkey 1");
                }
                else
                {
                    if (CurrentSelectable)
                    {
                        Debug.Log("Unselect 2");
                        CurrentSelectable.Deselect();
                        CurrentSelectable = null;
                        Hero.UnSelect();
                    }
                }
            }
            else
            {
                if (CurrentSelectable)
                {
                    Debug.Log("Unselect 3");
                    CurrentSelectable.Deselect();
                    CurrentSelectable = null;
                    Hero.UnSelect();
                }
            }
        }
    }
}