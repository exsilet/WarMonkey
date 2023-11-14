using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class SelectUnit : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;

        public Selectable CurrentSelectable;
        public HeroMover Hero;

        private Selectable _selectable;
        private RaycastHit _hit;
        private Ray _ray;
        private bool selecting;
        private Camera _camera;
        private Vector3 mouseStartPosition;
        private List<Selectable> _units = new();

        public void Construct(Selectable selectable)
        {
            _selectable = selectable;
            AddUnits();
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                selecting = true;
                mouseStartPosition = Input.mousePosition;

                _ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(_ray, out _hit))
                {
                    mouseStartPosition = _hit.point;
                }
            }

            if (Input.GetMouseButtonUp(1))
            {
                selecting = false;
                DeselectAll();
                Debug.Log(_hit.collider.gameObject.GetComponent<Selectable>());

                if (_hit.collider.gameObject.GetComponent<Selectable>())
                {
                    CurrentSelectable = _hit.collider.gameObject.GetComponent<Selectable>();
                    _hit.collider.gameObject.GetComponent<Selectable>().Select();
                    Hero = _hit.collider.gameObject.GetComponent<HeroMover>();
                }
            }
        }

        private void AddUnits()
        {
            _units.Add(_selectable);
        }

        private void SelectPart()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            mouseStartPosition = Input.mousePosition;

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                _hit = hit;
                mouseStartPosition = hit.point;
            }
        }

        private void SingleSelect(RaycastHit hit)
        {
            selecting = false;
            DeselectAll();

            if (mouseStartPosition == Input.mousePosition)
            {
                if (hit.collider.gameObject.GetComponent<HeroMover>())
                {
                    CurrentSelectable = hit.collider.gameObject.GetComponent<Selectable>();
                    hit.collider.gameObject.GetComponent<Selectable>().Select();
                    Hero = hit.collider.gameObject.GetComponent<HeroMover>();
                }
            }
        }

        private void DeselectAll()
        {
            foreach (var unit in _units)
            {
                unit.Deselect();
            }
        }
    }
}