using UnityEngine;

namespace Assets.Scripts.AuxiliaryComponents
{
    public class Camera : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float offset;

        private void LateUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, _target.position, Time.deltaTime * offset);
        }
    }
}