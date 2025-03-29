using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D), typeof(Animator))]
public class InteractComponent : MonoBehaviour
{
    [SerializeField] private UnityEvent onInteract;
    [SerializeField] private Animator _animator;
    
    private readonly int _interactTriggerHash = Animator.StringToHash("Interact");
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        onInteract?.Invoke();
        _animator.SetTrigger(_interactTriggerHash);
    }
    
}
