using System;
using UnityEngine;

namespace UI.Element
{
    public class ScrenShot : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKey(KeyCode.A))
            {
                ScreenCapture.CaptureScreenshot("Scene.png");
            }
        }
    }
}