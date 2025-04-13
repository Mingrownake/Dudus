using System;
using Components;
using UnityEditor.Animations;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _speed = 4f;
    [SerializeField] private float _jumpForce = 12f;
    [SerializeField] private float radiusInteract = 0.5f;
    [SerializeField] private int _damage = 1;
    
    [Header("Components")]
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private HealthComponent _healthComponent;
    [SerializeField] private ParticleCreatorComponent _particleCreatorComponent;
    [SerializeField] private CoinCollector _coinCollector;
    [SerializeField] private CheckCircleOverlap _checkCircleOverlap;
    [SerializeField] private ExplosionSpawnComponent _explosionSpawnComponent;
    
    [Header("State")]
    private StateManager _stateManager;
    
    [Header("Animations")]
    [SerializeField] private AnimatorController _unArmedAnimator;
    [SerializeField] private AnimatorController _armedAnimator;
    
    
    private readonly int IsRunningHash = Animator.StringToHash("IsRunning");
    private readonly int IsGroundedHash = Animator.StringToHash("IsGrounded");
    private readonly int VerticalVelocityHash = Animator.StringToHash("VerticalVelocity");
    private readonly int HitHash = Animator.StringToHash("Hit");
    private readonly int AttackHash = Animator.StringToHash("Attack");
    
    private Vector2 _direction;
    private bool PressedJump => _direction.y > 0;
    private bool IsGrounded => _groundChecker.IsGrounded;
    private bool _overFall = false;
    private bool _canDoubleJump = true;
    private bool _armed = false;
    private LayerMask _interactLayerMask;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _groundChecker = GetComponent<GroundChecker>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _healthComponent = GetComponent<HealthComponent>();
        _particleCreatorComponent = GetComponent<ParticleCreatorComponent>();
        _coinCollector = GetComponent<CoinCollector>();
        _checkCircleOverlap = GetComponent<CheckCircleOverlap>();
        _explosionSpawnComponent = GetComponent<ExplosionSpawnComponent>();
    }

    private void Start()
    {
        _interactLayerMask = LayerMask.GetMask("Interactable");
        _stateManager = FindAnyObjectByType<StateManager>();
        HasArm();
        _coinCollector.InitStateManager(_stateManager);
        _healthComponent.SetHealth(_stateManager.Data.health);
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

    public void HealthEffect(int health)
    {
        Debug.Log($"Health: {health}");
        _stateManager.Data.health = health;
    }

    public void PickCoin(int amount)
    {
        _coinCollector.CollectCoins(amount);
    }

    public void ApplyInteract()
    {
        var hit = Physics2D.OverlapCircle(transform.position, radiusInteract, _interactLayerMask);
        if (hit != null && hit.TryGetComponent<InteractComponent>(out InteractComponent interact))
        {
            interact.Interact();
        }
    }

    private void ApplyDamage()
    {
        _animator.SetTrigger(HitHash);
        _rigidbody2D.AddForceY(_jumpForce * 4, ForceMode2D.Impulse);
        int coins = _coinCollector.Coins;
        if (coins >= 5)
        {
            int removingCoins = 5;
            SetBurst(removingCoins);
        }
        else if (coins > 0)
        {
            int removingCoins = Random.Range(1, _coinCollector.Coins);
            SetBurst(removingCoins);
        }
    }

    private void SetBurst(int amount)
    {
        _coinCollector.RemoveCoins(amount);
        ParticleSystem _particleFallCoins = _particleCreatorComponent.SpawnCoinsFallParticles();
        var burst = _particleFallCoins.emission.GetBurst(0);
        burst.count = amount;
        _particleFallCoins.emission.SetBurst(0, burst);
        _particleFallCoins.Play();
    }

    public void ApplyFall()
    {
        if (_overFall)
        {
            this._particleCreatorComponent.SpawnFallParticles();
            _overFall = false;
        }
    }

    private void FixedUpdate()
    {
        SetHorizontalVelocity();
        SetVerticalVelocity();
        SetAnimation();
        HorizontalOrientation();
        if (IsGrounded)
        {
            _canDoubleJump = true;
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
            } else if (!IsGrounded && _rigidbody2D.linearVelocity.y < -1 && _canDoubleJump)
            {
                _rigidbody2D.linearVelocityY = _jumpForce;
                _canDoubleJump = false;
                _overFall = true;
            }
        } else if (_rigidbody2D.linearVelocityY > 0)
        {
            _rigidbody2D.linearVelocity = new Vector2(_rigidbody2D.linearVelocityX, _rigidbody2D.linearVelocityY * 0.5f);
        }

        if (_rigidbody2D.linearVelocityY > _jumpForce)
        {
            _rigidbody2D.linearVelocityY = _jumpForce;
        }

        if (_rigidbody2D.linearVelocityY < -15)
        {
            _overFall = true;
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
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (_direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void Armed()
    {
        if (!_armed) _stateManager.Data.SetArmed(true);
        HasArm();
    }
    

    public void PlayAttackAnimation()
    {
        if (!_armed) return;
        _animator.SetTrigger(AttackHash);
    }

    private void HasArm()
    {
        _armed = _stateManager.Data.isArmed;
        if (_armed)
        {
            _animator.runtimeAnimatorController = _armedAnimator; 
        }
    }

    public void Attack()
    {
        var colliders = _checkCircleOverlap.CheckCircles();
        _explosionSpawnComponent.Spawn();
        if (colliders.Length == 0) return;
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].ApplyEffect(_damage, EffectType.Damage);
        }
    }
}
