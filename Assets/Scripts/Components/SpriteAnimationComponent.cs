using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimationComponent : MonoBehaviour
{
    [SerializeField] private AnimNode[] _nodes;
    
    private AnimNode _currentNode;
    private int _currentSpriteIndex;
    private int _currentNodeIndex = 0;
    private float _speedAnimation;
    private float _nextAnimation;
    
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        if (_nodes.Length == 0)
        {
            Destroy(this);
        }
        _currentNode = _nodes[_currentNodeIndex];
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _speedAnimation = 1 / _currentNode._countAnimationInSeconds;
    }

    private void OnEnable()
    {
        _currentSpriteIndex = 0;
        _spriteRenderer.sprite = _currentNode._sprites[_currentSpriteIndex];
        _nextAnimation = Time.time + _speedAnimation;
    }

    private void Update()
    {
        if (Time.time < _nextAnimation) return;
        if (_currentSpriteIndex < _currentNode._sprites.Length - 1)
        {
            _currentSpriteIndex++;
            _spriteRenderer.sprite = _currentNode._sprites[_currentSpriteIndex];
            _nextAnimation += _speedAnimation;
        }
        else
        {
            if (_currentNode._isLoop)
            {
                _currentSpriteIndex = 0;
            }
            else
            {
                _currentNode.OnAnimationFinished?.Invoke();
            }
        }
    }

    public void NextNode(String name)
    {
        AnimNode nextNode = null;
        for (int i = 0; i < _nodes.Length; i++)
        {
            if (_nodes[i]._name == name)
            {
                nextNode = _nodes[i];
            }
        }
        
        if (nextNode != null)
        {
            _currentNode = nextNode;
        }
    }

    
    [Serializable]
    private class AnimNode
    {
        [Header("Node Settings")]
        [SerializeField] public Sprite[] _sprites;
        [SerializeField] public float _countAnimationInSeconds;
        [SerializeField] public bool _isLoop;
        [SerializeField] public string _name;
        [SerializeField] public AnimNode _nextNode;
        
        [Header("Node Actions")]
        [SerializeField] public UnityEvent OnAnimationFinished;
    }
}
