using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CircleRenderer : MonoBehaviour
{
    [SerializeField] private ParticleSystem _circle;
    [SerializeField] private List<Transform> _targetsToDetect;

    private float _distanceToNearestTarget;
    private List<float> _distancesToTargets = new List<float>();
    private float _distanceToDisableCircle = 1.2f;
    private float _redDistance = 30;
    private float _yellowDistance = 20;
    private  float _greenDistance = 10;

    private void Start()
    {
        DetermineStartDistances();
    }

    private void Update()
    {
        DetermineDistancesToTargets();
        DetermineNearestTarget();
        RemoveCollectedTarget();
        Render();  
    }



    private void DetermineNearestTarget()
    {   
        _distanceToNearestTarget = _distancesToTargets.Min();
    }

    private void DetermineStartDistances()
    {
        for (int i = 0; i < _targetsToDetect.Count; i++)
        {
            if (_targetsToDetect[i] != null)
            {
                _distancesToTargets.Add(Vector3.Distance(transform.position, _targetsToDetect[i].position));
            }
        }
    }

    private void RemoveCollectedTarget()
    {
        int indexToRemove;
        
        if(_distancesToTargets.Min() <= _distanceToDisableCircle)
        {
            indexToRemove = _distancesToTargets.IndexOf(_distancesToTargets.Min());
            _distancesToTargets.RemoveAt(indexToRemove);
            _targetsToDetect.RemoveAt(indexToRemove);    
        }
    }

    private void EnableCircle()
    {
        _circle.gameObject.SetActive(true);
    }

    private void DisableCircle()
    {
        _circle.gameObject.SetActive(false);
    }

    [System.Obsolete]
    public void Render()
    {
        if (_distanceToNearestTarget > _redDistance || _distanceToNearestTarget <= _distanceToDisableCircle)
            DisableCircle();
        else
            EnableCircle();

        if (_distanceToNearestTarget <= _redDistance && _distanceToNearestTarget > _yellowDistance)
            _circle.startColor = Color.red;

        if (_distanceToNearestTarget <= _yellowDistance && _distanceToNearestTarget > _greenDistance)
            _circle.startColor = Color.yellow;

        if (_distanceToNearestTarget <= _greenDistance)
            _circle.startColor = Color.green;
    }

    public void DetermineDistancesToTargets()
    {
        for (int i = 0; i < _targetsToDetect.Count; i++)
        {
            if (_targetsToDetect[i] != null)
            {
                _distancesToTargets[i] = Vector3.Distance(transform.position, _targetsToDetect[i].position);
            }
        }
    }
}
