using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class IconOfDoor : MonoBehaviour
{
    private const string CanBecomeGreen = "CanBecomeGreen";

    [SerializeField] private int _id;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Door.Opened += StartOpenedAnimation;
    }
    private void OnDisable()
    {
        Door.Opened -= StartOpenedAnimation;
    }

    public void StartOpenedAnimation(int id)
    {
        if(id == _id)
            _animator.SetBool(CanBecomeGreen, true);
    }        
}
