using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TargetFollow))]  
public class Arrow : MonoBehaviour
{
    [SerializeField] private List<Door> _doors;
    [SerializeField] private Player _player;

    private void Update()
    {
         Vector3 direction = _doors[_player.CurrentKeyID].transform.position - transform.position;

        if (_player.IsHaveKey)
                 transform.rotation = Quaternion.LookRotation(direction,Vector3.up);
    }
}

