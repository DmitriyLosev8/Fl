using UnityEngine;

namespace Assets.Scripts.GUI
{
    public class ImageRotator : MonoBehaviour
    {
        private float _rotateValue = -0.5f;

        private void Update()
        {
            transform.Rotate(0, 0, _rotateValue);
        }
    }
}