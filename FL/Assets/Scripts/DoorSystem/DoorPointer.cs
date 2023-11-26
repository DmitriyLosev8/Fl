using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.AuxiliaryComponents;
using Assets.Scripts.InteractiveObjects.Character;

namespace Assets.Scripts.DoorSystem
{
    [RequireComponent(typeof(TargetFollow))]
    public class DoorPointer : MonoBehaviour
    {
        [SerializeField] private List<Door> _doors;
        [SerializeField] private Player _player;

        private void Update()
        {    
            Vector3 direction = _doors[_player.CurrentKeyID].transform.position - transform.position;

            if (_player.IsHaveKey)
            {
                transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
            }
        }
    }
}