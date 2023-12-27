using UnityEngine;

namespace Player
{
    public class DragPlayer : MonoBehaviour
    {
        public Vector3 MousePosition;

        private Vector3 GetMousePos()
        {
            return Camera.main.WorldToScreenPoint(transform.position);
        }

        private void OnMouseDown()
        {
            MousePosition = Input.mousePosition - GetMousePos();
        }

        private void OnMouseDrag()
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - MousePosition);
        }
    }
}