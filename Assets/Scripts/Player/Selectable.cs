using UnityEngine;

namespace Player
{
    public class Selectable : MonoBehaviour
    {
        public void Select()
        {
            GetComponent<Renderer>().material.color = Color.yellow;
        }

        public void Deselect()
        {
            GetComponent<Renderer>().material.color = Color.clear;
        }
    }
}