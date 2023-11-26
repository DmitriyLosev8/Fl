using UnityEngine;

namespace Assets.Scripts.AuxiliaryComponents
{
    public class JoyStickEnabler : MonoBehaviour
    {
        [SerializeField] private GameObject _joyStick;

        private void Start()
        {
            if (Application.isMobilePlatform)
            {
                EnableJoistick();
            }         
        }

        private void EnableJoistick()
        {
            _joyStick.SetActive(true);
        }
    }
}