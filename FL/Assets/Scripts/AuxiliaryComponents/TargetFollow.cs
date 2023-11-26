using UnityEngine;

namespace Assets.Scripts.AuxiliaryComponents
{
    public class TargetFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _offsetPositionY;

        private void LateUpdate()
        {
            transform.position = new Vector3(_target.position.x, _target.position.y + _offsetPositionY, _target.position.z);
        }
    }
}