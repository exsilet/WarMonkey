using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class Selectable : MonoBehaviour
    {
        [SerializeField] private Image _select;
        
        private bool _selected = false;

        public bool Selected => _selected;

        public void Select()
        {
            _selected = true;
            _select.gameObject.SetActive(true);
        }

        public void Deselect()
        {
            _selected = false;
            _select.gameObject.SetActive(false);
        }
    }
}