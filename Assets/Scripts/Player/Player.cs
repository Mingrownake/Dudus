using System;
using Components;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _speed = 4f;
    [SerializeField] private float _jumpForce = 12f;
    [SerializeField] private float radiusInteract = 0.5f;
    
    [Header("Components")]
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private HealthComponent _healthComponent;
    
    private readonly int IsRunningHash = Animator.StringToHash("IsRunning");
    private readonly int IsGroundedHash = Animator.StringToHash("IsGrounded");
    private readonly int VerticalVelocityHash = Animator.StringToHash("VerticalVelocity");
    private readonly int HitHash = Animator.StringToHash("Hit");
    
    private Vector2 _direction;
    private bool PressedJump => _direction.y > 0;
    private bool IsGrounded => _groundChecker.IsGrounded;

    private bool CanDoubleJump = true;
    private LayerMask _interactLayerMask;
    

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _groundChecker = GetComponent<GroundChecker>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _healthComponent = GetComponent<HealthComponent>();
        
    }

    private void Start()
    {
        _interactLayerMask = LayerMask.GetMask("Interactable");
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    public void ApplyEffect(int amount, EffectType type)
    {
        _healthComponent.ApplyEffect(amount, type);
        switch (type)
        {
            case EffectType.Damage:
                ApplyDamage();
                break;
        }
    }

    public void ApplyInteract()
    {
        var hit = Physics2D.OverlapCircle(transform.position, radiusInteract, _interactLayerMask);
        if (hit.TryGetComponent<InteractComponent>(out InteractComponent interact))
        {
            interact.Interact();
        }
    }

    private void ApplyDamage()
    {
        _animator.SetTrigger(HitHash);
        _rigidbody2D.linearVelocityY = _jumpForce * 4;
    }

    private void FixedUpdate()
    {
        SetHorizontalVelocity();
        SetVerticalVelocity();
        SetAnimation();
        HorizontalOrientation();
        if (IsGrounded)
        {
            CanDoubleJump = true;
        }
        
    }

    private void SetHorizontalVelocity()
    {
        _rigidbody2D.linearVelocityX = _direction.x * _speed;
    }

    private void SetVerticalVelocity()
    {
        if (PressedJump)
        {
            if (IsGrounded && _rigidbody2D.linearVelocity.y <= 0.01)
            {
                _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            } else if (!IsGrounded && _rigidbody2D.linearVelocity.y <= 0.01 && CanDoubleJump)
            {
                _rigidbody2D.linearVelocityY = _jumpForce;
                CanDoubleJump = false;
            }
        } else if (_rigidbody2D.linearVelocityY > 0)
        {
            _rigidbody2D.linearVelocity = new Vector2(_rigidbody2D.linearVelocityX, _rigidbody2D.linearVelocityY * 0.5f);
        }
    }

    private void SetAnimation()
    {
        _animator.SetBool(IsRunningHash, _direction.x != 0);
        _animator.SetBool(IsGroundedHash, IsGrounded);
        _animator.SetFloat(VerticalVelocityHash, _rigidbody2D.linearVelocityY);
    }

    private void HorizontalOrientation()
    {
        if (_direction.x > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if (_direction.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
    }
    
    
}
