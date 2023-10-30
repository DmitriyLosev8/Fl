using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (_counterOfTime > 0)
            _counterOfTime -= Time.deltaTime;

        if (_counterOfTime < 0)
            _counterOfTime = 0;

        if (_counterOfTime == 0)
        {
            int randomSound = Random.Range(0, _stepSounds.Count);
            _audioSource.PlayOneShot(_stepSounds[randomSound]);
            _counterOfTime = _delay;
        }
    }
}
