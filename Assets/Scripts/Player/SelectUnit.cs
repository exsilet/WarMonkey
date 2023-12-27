using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class SelectUnit : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;

        public Selectable CurrentSelectable;
        public HeroMover Hero;

        private GameObject _selectedObject;
        private HeroAttack _heroAttack;
        private HeroMover _heroMover;

        private Selectable _selectable;
        private bool _selecting;
        private bool _isDragEntity;
        private Camera _camera;
        private Vector3 _mouseDelta;
        private Transform _selected;
        private Vector3 _prefMousePosition;
        private List<Selectable> _units = new();
        private Ray TouchRay => _camera.ScreenPointToRay(Input.mousePosition);

        public void Construct(Selectable selectable)
        {
            _selectable = selectable;
            AddUnits();
        }

        private void Awake() => _camera = Camera.main;

        private void Update()
        {
            DragObject();
            
            if (Input.GetMouseButtonDown(0))
            {
                SingleSelect();
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (_heroAttack != null)
                {
                    _heroAttack.ShootAttack();
                    _heroAttack = null;
                    _selectedObject.GetComponent<Rigidbody>().useGravity = true;
                    _selectedObject = null;
                    _heroMover = null;
                    CurrentSelectable = null;
                }
            }
            
            Debug.DrawRay(_camera.transform.position, _camera.transform.forward * 100f, Color.blue);
        }

        private void AddUnits()
        {
            _units.Add(_selectable);
        }

        private void DragObject()
        {
            if (_selectedObject != null)
            {
                Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                    _camera.WorldToScreenPoint(_selectedObject.transform.position).z);
                Vector3 worldPosition = _camera.ScreenToWorldPoint(position);
                _selectedObject.transform.position = new Vector3(worldPosition.x, 0.45f, worldPosition.z);
            }
        }

        private void SingleSelect()
        {
            Vector2 clickPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            if (Physics.Raycast(TouchRay, out var hit))
            {
                Debug.Log(hit.collider + " select");

                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.GetComponent<Selectable>())
                    {
                        CurrentSelectable = hit.collider.gameObject.GetComponent<Selectable>();
                        CurrentSelectable.Select();
                        _selected = hit.transform;
                        //_selected.transform.position = clickPosition;
                        _selectedObject = hit.collider.gameObject;
                        _selectedObject.GetComponent<Rigidbody>().useGravity = false;
                        _heroAttack = hit.collider.GetComponent<HeroAttack>();
                        _heroAttack.StartAttack();
                    }
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