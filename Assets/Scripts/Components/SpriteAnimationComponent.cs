using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimationComponent : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private float _countAnimationInSeconds;
    [SerializeField] private bool _isLoop;
    
    [Header("Actions")]
    [SerializeField] private UnityEvent OnAnimationFinished;
    
    private int _currentSpriteIndex;
    private float _speedAnimation;
    private float _nextAnimation;
    
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
     _spriteRenderer = GetComponent<SpriteRenderer>();
     _speedAnimation = 1 / _countAnimationInSeconds;
    }

    private void OnEnable()
    {
        _currentSpriteIndex = 0;
        _spriteRenderer.sprite = _sprites[_currentSpriteIndex];
        _nextAnimation = Time.time + _speedAnimation;
    }

    private void Update()
    {
        if (Time.time < _nextAnimation) return;
        if (_currentSpriteIndex < _sprites.Length - 1)
        {
            _currentSpriteIndex++;
            _spriteRenderer.sprite = _sprites[_currentSpriteIndex];
            _nextAnimation += _speedAnimation;
        }
        else
        {
            if (_isLoop)
            {
                _currentSpriteIndex = 0;
            }
            else
            {
                OnAnimationFinished?.Invoke();
            }
        }
    }
}
