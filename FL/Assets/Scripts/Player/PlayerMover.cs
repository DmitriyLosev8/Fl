using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(Animator))]
public class PlayerMover : MonoBehaviour
{
    private const string IsRunning = "IsRunning";
    private const string IsClimbing = "IsClimbing";

    [SerializeField] private float _speed = 4.5f;
    [SerializeField] private float _rotationSensetivity;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _latern;
    [SerializeField] private PlayersSounds _playersSounds;

    private PlayerInput _playerInput;
    private Vector2 _moveInput;
    private Rigidbody _rigidbody;
    private bool _isOnLadder;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
   
    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();
    }

    private void Update()
    {
        _moveInput = _playerInput.Movement.Move.ReadValue<Vector2>();
        _playerInput.Movement.Move.canceled += ctx => _moveInput = Vector2.zero;
    }

    void FixedUpdate()
    {
        if (_isOnLadder)
            MoveUp();
        else
            Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Ladder ladder))
        {
            _rigidbody.position = ladder.SpotToStart.transform.position;
            _rigidbody.useGravity = false;
            _isOnLadder = true;
            _rigidbody.transform.LookAt(Vector3.zero);
            _animator.SetBool(IsClimbing, _isOnLadder);
            _latern.gameObject.SetActive(false);
        }
    }

    private void MoveUp()
    {
        float speedOfMoveUp = 0.05f;
        float heightToUp = 50;
       
        Vector3 targetPosition = new Vector3(_rigidbody.position.x, _rigidbody.position.y + heightToUp, _rigidbody.position.z);
        _rigidbody.position = Vector3.MoveTowards(_rigidbody.position, targetPosition, speedOfMoveUp);
    }

    public void Move()  
    {   
            Vector3 moveDirection = new Vector3(_moveInput.x, 0, _moveInput.y);

            if (moveDirection.x != 0 || moveDirection.z != 0)
            {
                Quaternion rotationAngle = Quaternion.LookRotation(moveDirection);
                _rigidbody.rotation = Quaternion.Lerp(transform.rotation, rotationAngle, Time.deltaTime * _rotationSensetivity);
                _animator.SetBool(IsRunning, true);
                _playersSounds.PlayStepSound();
                 Vector3 velocity = moveDirection * _speed;
                _rigidbody.velocity = velocity;    
             }
             else
                     _animator.SetBool(IsRunning, false);
    }      
}
