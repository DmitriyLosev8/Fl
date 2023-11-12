using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class ResourceOrb : MonoBehaviour
{
    [SerializeField] private float _oxygenValueToGive;
    [SerializeField] private float _lightValueToGive;
    [SerializeField] private bool _isOxygen;

    private bool _isSpoted = false;
    private Player _player;
    private Vector3 _targetPosition;
    private float _speed = 5.8f;
    private bool _isDestroyed;
    private AudioSource _audioSource;
 
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_player != null)
            BecomeCollected(_player);
    }

    public void DeterminePlayer(Player player)
    {
        _player = player;
    }
   
    private void BecomeCollected(Player player)
    {
        DetermineTargetPosition();

        _isSpoted = player.IsSpotedResoursesOrb;

        if (_isSpoted)
         transform.position = Vector3.MoveTowards(transform.position,_targetPosition, _speed * Time.deltaTime);

        if (transform.position == _targetPosition)
        {   
            if (_isOxygen)
                player.TakeOxygen(_oxygenValueToGive);
            else
                player.TakeLight(_lightValueToGive);  
           
            _isDestroyed = true;
            PlayCollectedSound();
            Destroy(gameObject);
        }      
    }

    private void DetermineTargetPosition()
    {
        float offsetY = 1;
        _targetPosition = new Vector3(_player.transform.position.x, _player.transform.position.y + offsetY, _player.transform.position.z); 
    }

    private void PlayCollectedSound()
    {
        float volume = 0.4f;
        
        if (_isDestroyed)
            AudioSource.PlayClipAtPoint(_audioSource.clip, transform.position, volume);
    }
}
