using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float offset;

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _target.position, Time.deltaTime * offset);
    }
}
