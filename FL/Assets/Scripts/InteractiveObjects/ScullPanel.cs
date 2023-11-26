using UnityEngine;
using Assets.Scripts.InteractiveObjects.Character;

namespace Assets.Scripts.InteractiveObjects
{
    public class ScullPanel : MonoBehaviour
    {
        [SerializeField] private Light _leftEye;
        [SerializeField] private Light _rightEye;
        [SerializeField] private GameObject _deadlyEnemy;

        private int _valueOfLightOn = 30;
        private int _valueOfLightOff = 0;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                TurnLightOn();
                _deadlyEnemy.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                TurnLightOff();
                _deadlyEnemy.SetActive(false);
            }
        }

        private void TurnLightOn()
        {
            _leftEye.intensity = _valueOfLightOn;
            _rightEye.intensity = _valueOfLightOn;
        }

        private void TurnLightOff()
        {
            _leftEye.intensity = _valueOfLightOff;
            _rightEye.intensity = _valueOfLightOff;
        }
    }
}