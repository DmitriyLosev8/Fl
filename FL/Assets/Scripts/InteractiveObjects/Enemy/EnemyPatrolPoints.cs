using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.InteractiveObjects.Character;

namespace Assets.Scripts.InteractiveObjects.Enemy
{
    public class EnemyPatrolPoints : MonoBehaviour
    {
        [SerializeField] private List<Transform> _points;

        private bool _isPlayerNear;

        public List<Transform> Points => _points;
        public bool IsPlayerNear => _isPlayerNear;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                _isPlayerNear = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                _isPlayerNear = false;
            }
        }
    }
}