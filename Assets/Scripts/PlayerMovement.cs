using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private InputControls _inputControls;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _moveVector;
    [SerializeField] private float jumpVelocity = 20.0f;
    [SerializeField] private float moveSpeed = 1.0f;
    private static readonly int _isRunning = Animator.StringToHash("isRunning");
    private bool _isGrounded;
    
    private void Awake()
    {
        _inputControls = new InputControls();
        _inputControls.Player.Jump.performed += context => OnJumpPerformed();
        _inputControls.Player.Jump.canceled += context => OnJumpCanceled();
        _inputControls.Player.Move.performed += context => _moveVector = context.ReadValue<Vector2>();
        _inputControls.Player.Move.canceled += context => _moveVector = Vector2.zero;

        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void OnEnable()
    {
        _inputControls.Player.Enable();
    }

    private void OnDisable()
    {
        _inputControls.Player.Disable();
    }

    private void FixedUpdate()
    {
        OnMovePerformed();
    }

    private void OnJumpPerformed()
    {
#if UNITY_EDITOR
        Debug.Log("OnJumpPerformed");
#endif
        if(_isGrounded)
            _rigidbody2D.AddForce(Vector2.up * jumpVelocity);
    }

    private void OnMovePerformed()
    {
        var movement = new Vector3(_moveVector.x, 0.0f, _moveVector.y) * (moveSpeed * Time.fixedDeltaTime);

        transform.Translate(movement);
        if (movement.x == 0)
        {
            _animator.SetBool(_isRunning, false);
        }
        else if(movement.x > 0)
        {
            _animator.SetBool(_isRunning, true);
            _spriteRenderer.flipX = false;
        }
        else
        {
            _animator.SetBool(_isRunning, true);
            _spriteRenderer.flipX = true;
        }
#if UNITY_EDITOR
        if (movement.x != 0)
        {
            Debug.Log($"Movement: {movement.x.ToString()} {movement.y.ToString()}");
        }        
#endif
    }

    private void OnJumpCanceled()
    {
        _rigidbody2D.AddForce(Vector2.zero);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Terrian"))
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Terrian"))
        {
            _isGrounded = false;
        }
    }
}
