using UnityEngine;

namespace Assets.Scripts.InteractiveObjects
{
    public class Ladder : MonoBehaviour
    {
        [SerializeField] private GameObject _spotToStart;

        public GameObject SpotToStart => _spotToStart;
    }
}