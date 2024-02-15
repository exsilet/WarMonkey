using System;
using System.Collections.Generic;
using Logic;
using UI.Element;
using UnityEngine;

namespace Player
{
    public class SelectUnit : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;

        private float _frontBorder = -6;
        private float _backBorder = -16;
        private float _leftBorder = -4.2f;
        private float _rightBorder = 20;
        private float _swapBorder;

        public Selectable CurrentSelectable;

        private GameObject _selectedObject;
        private Shooting _heroAttack;
        private StartBattle _startBattle;

        private Selectable _selectable;
        private bool _selecting;
        private bool _isDragEntity;
        private Camera _camera;
        private Vector3 _mouseDelta;
        private Transform _selected;
        private Vector3 _prefMousePosition;
        private List<Selectable> _units = new();
        private Ray TouchRay => _camera.ScreenPointToRay(Input.mousePosition);

        public void Construct(Selectable selectable, StartBattle startBattle)
        {
            _selectable = selectable;
            _startBattle = startBattle;
            AddUnits();
        }

        private void Awake() => _camera = Camera.main;

        private void Update()
        {
            if (_startBattle.CurrentStartBattle)
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
                        //_heroAttack.ShootAttack();
                        UnSelected();
                    }
                }
            }
        }

        private void AddUnits()
        {
            _units.Add(_selectable);
        }

        private void UnSelected()
        {
            _heroAttack.StopCharging();
            _heroAttack = null;
            _selectedObject.GetComponent<Rigidbody>().useGravity = true;
            _selectedObject = null;
            CurrentSelectable.Deselect();
            CurrentSelectable = null;
        }

        private void DragObject()
        {
            if (_selectedObject != null)
            {
                Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                    _camera.WorldToScreenPoint(_selectedObject.transform.position).z);
                Vector3 worldPosition = _camera.ScreenToWorldPoint(position);

                if (worldPosition.z < _frontBorder && worldPosition.z > _backBorder && worldPosition.x < _rightBorder &&
                    worldPosition.x > _leftBorder)
                {
                    _selectedObject.transform.position = new Vector3(worldPosition.x, 0.45f, worldPosition.z);
                }
                else if (worldPosition.x < _rightBorder && worldPosition.x > _leftBorder)
                {
                    _swapBorder = _frontBorder - worldPosition.z;
                    float swapTop = worldPosition.x -_swapBorder;
                    
                    if (swapTop < _rightBorder)
                    {
                        _selectedObject.transform.position = new Vector3(swapTop, 0.45f, _frontBorder);
                    }
                }
            }
        }

        private void SingleSelect()
        {
            Vector2 clickPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            if (Physics.Raycast(TouchRay, out var hit))
            {
                if (hit.collider != null)
                {
                    Debug.Log("select + " + hit.collider);

                    if (hit.collider.gameObject.GetComponent<Selectable>())
                    {
                        CurrentSelectable = hit.collider.gameObject.GetComponent<Selectable>();
                        CurrentSelectable.Select();
                        _selected = hit.transform;
                        _selectedObject = hit.collider.gameObject;
                        _selectedObject.GetComponent<Rigidbody>().useGravity = false;
                        //_heroAttack = hit.collider.GetComponent<HeroAttack>();
                        _heroAttack = hit.collider.GetComponent<Shooting>();
                        //_heroAttack.StartAttack();
                        _heroAttack.StartCharging();
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