using UnityEngine;

namespace Assets.Scripts.DoorSystem
{
    public class Key : MonoBehaviour
    {
        [SerializeField] private int _id;

        public int Id => _id;
    }
}