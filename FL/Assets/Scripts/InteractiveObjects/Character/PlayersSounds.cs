using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.InteractiveObjects.Character
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayersSounds : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> _stepSounds;

        private float _delay = 0.28f;
        private float _counterOfTime;
        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayStepSound()
        {
            switch (_counterOfTime)
            {
                case 0:
                    int randomSound = Random.Range(0, _stepSounds.Count);
                    _audioSource.PlayOneShot(_stepSounds[randomSound]);
                    _counterOfTime = _delay;
                    break;
                case > 0:
                    _counterOfTime -= Time.deltaTime;
                    break;
                case < 0:
                    _counterOfTime = 0;
                    break;
            }         
        }
    }
}