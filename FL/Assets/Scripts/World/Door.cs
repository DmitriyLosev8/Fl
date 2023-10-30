using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class Door : MonoBehaviour
{
    private const string CanOpen = "CanOpen";

    [SerializeField] private int _id;

    private Animator _animator;
    private AudioSource _audiosourse;

    public int Id => _id;
    public static event UnityAction<int> Opened;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audiosourse = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Key key))
        {
            if (key.Id == _id)
            {
                _animator.SetBool(CanOpen, true);
                _audiosourse.Play();
            }      
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Key key))
        {
            if (key.Id == _id)
            {
                Destroy(key.gameObject);
                Opened?.Invoke(_id);
            }   
        }
    }
}
