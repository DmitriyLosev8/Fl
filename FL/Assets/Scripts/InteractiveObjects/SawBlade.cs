using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBlade : MonoBehaviour
{
    [SerializeField] private Transform _point1;
    [SerializeField] private Transform _point2;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _damage;

    private Vector3 _targetPoint;

    private void Start()
    {
        transform.position = _point1.position;
    }

    private void Update()
    {
        if (transform.position == _point2.position)
            _targetPoint = _point1.position;

        if (transform.position == _point1.position)
            _targetPoint = _point2.position;

        transform.position = Vector3.MoveTowards(transform.position, _targetPoint, _speed * Time.deltaTime);
        transform.Rotate(0, 0, transform.rotation.z + _rotateSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
            player.TakeHealthDamage(_damage);
    }
}
