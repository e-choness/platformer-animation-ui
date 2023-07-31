
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private enum MovementState
    {
        Idle,
        Running,
        Jumping,
        Falling
    }
    
    private InputControls _inputControls;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _moveVector;
    [SerializeField] private float jumpForce = 20.0f;
    [SerializeField] private float moveSpeed = 1.0f;
    private static readonly int MovingState = Animator.StringToHash("State");
    private bool _isGrounded;
    

    private void Awake()
    {
        _inputControls = new InputControls();
        _inputControls.Player.Jump.performed += context => OnJumpPerformed();
        _inputControls.Player.Jump.canceled += context => OnJumpCanceled();
        _inputControls.Player.Move.performed += context => _moveVector = context.ReadValue<Vector2>();
        _inputControls.Player.Move.canceled += context => _moveVector = Vector2.zero;

        // _movementState = MovementSate.Idle;
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
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpForce);

#if UNITY_EDITOR
        Debug.Log("OnJumpPerformed");
#endif
        
    }

    private void OnMovePerformed()
    {

        _rigidbody2D.velocity = new(_moveVector.x * moveSpeed, _rigidbody2D.velocity.y);
        if (_moveVector.x == 0)
        {
            if(_isGrounded)
                UpdateAnimation(MovementState.Idle);
        }
        else if(_moveVector.x > 0)
        {
            if(_isGrounded)
                UpdateAnimation(MovementState.Running);
            _spriteRenderer.flipX = false;
        }
        else
        {
            if(_isGrounded)
                UpdateAnimation(MovementState.Running);
            _spriteRenderer.flipX = true;
        }

        if (!_isGrounded)
        {
            UpdateAnimation(_rigidbody2D.velocity.y > 0.1f ? MovementState.Jumping : MovementState.Falling);
        }
            
#if UNITY_EDITOR
        if (_moveVector.x != 0)
        {
            Debug.Log($"Movement: {_moveVector.x.ToString()} {_moveVector.y.ToString()}");
        }        
#endif
    }

    private void UpdateAnimation(MovementState state)
    {
        switch (state)
        {
            case MovementState.Idle:
                _animator.SetInteger(MovingState, (int)state);
                break;    
            case MovementState.Running:
                _animator.SetInteger(MovingState, (int)state);
                break;
            case MovementState.Jumping:
                _animator.SetInteger(MovingState, (int)state);
                break;
            case MovementState.Falling:
                _animator.SetInteger(MovingState, (int)state);
                break;
            default:
                break;
        }
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
