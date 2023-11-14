using UnityEngine;

namespace Player
{
    public class Selectable : MonoBehaviour
    {
        private bool _selected = false;

        public bool Selected => _selected;

        public void Select()
        {
            _selected = true;
        }

        public void Deselect()
        {
            _selected = false;
        }
    }
}