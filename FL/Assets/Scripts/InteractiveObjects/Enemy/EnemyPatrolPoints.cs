using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolPoints : MonoBehaviour
{
    [SerializeField] private List<Transform> _points;
    
    public List<Transform> Points => _points;
}
