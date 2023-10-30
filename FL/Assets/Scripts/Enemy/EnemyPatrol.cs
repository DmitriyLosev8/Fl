using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private EnemyPatrolPoints _points;

    private Vector3 _targetPosition;
    private int _randomIndexOfPoint;

    private void OnEnable()
    {
        DetermineRandomPoint();
        SetRandomSpeed();
        transform.position = _points.Points[_randomIndexOfPoint].position;
        StartCoroutine(WaitForReachTargetPoint());
    }

    private void Update()
    {
        Move();
    }

    private void DetermineRandomPoint()
    {
        int firstPoint = 0;
        _randomIndexOfPoint = Random.Range(firstPoint, _points.Points.Count);
    }

    private IEnumerator WaitForReachTargetPoint()
    { 
        while (true)
        {
            DetermineRandomPoint();  
            _targetPosition = _points.Points[_randomIndexOfPoint].position;
            yield return new WaitUntil(() => transform.position == _targetPosition);
        }      
    }
    private void SetRandomSpeed()
    {
        float minSpeed = 6;
        float maxSpeed = 12;
        _speed = Random.Range(minSpeed, maxSpeed);
    }
    private void Move()
    {
        if (transform.position != _targetPosition)
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
    }
}
