using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D), typeof(Animator))]
public class InteractComponent : MonoBehaviour
{
    [SerializeField] private UnityEvent onInteract;
    [SerializeField] private Animator _animator;
    [SerializeField] private TextMeshPro _hint;
    
    private readonly int _interactTriggerHash = Animator.StringToHash("Interact");

    private void OnTriggerEnter2D(Collider2D other)
    {
        _hint.renderer.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _hint.renderer.enabled = false;
    }

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
