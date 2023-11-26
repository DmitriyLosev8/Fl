using System;
using UnityEngine;

namespace Assets.Scripts.DoorSystem
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(AudioSource))]

    public class Door : MonoBehaviour
    {
        private const string CanOpen = "CanOpen";

        [SerializeField] private int _id;

        private Animator _animator;
        private AudioSource _audiosourse;

        public static event Action<int> Opened;

        public int Id => _id;

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
}