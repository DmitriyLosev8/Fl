using UnityEngine;
using System.Collections;

namespace Assets.Scripts.InteractiveObjects.Enemy
{
    public class EnemyPatrol : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private EnemyPatrolPoints _points;

        private Vector3 _targetPosition;
        private int _randomIndexOfPoint;
        private float _minSpeed = 6;
        private float _maxSpeed = 12;
        private int _firstPoint = 0;

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
            _randomIndexOfPoint = Random.Range(_firstPoint, _points.Points.Count);
        }

        private IEnumerator WaitForReachTargetPoint()
        {
            while (_points.IsPlayerNear)
            {
                DetermineRandomPoint();
                _targetPosition = _points.Points[_randomIndexOfPoint].position;
                yield return new WaitUntil(() => transform.position == _targetPosition);
            }
        }

        private void SetRandomSpeed()
        {
            _speed = Random.Range(_minSpeed, _maxSpeed);
        }

        private void Move()
        {
            if (transform.position != _targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
            }       
        }
    }
}