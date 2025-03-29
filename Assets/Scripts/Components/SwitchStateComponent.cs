using System;
using UnityEngine;

public class SwitchStateComponent : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private readonly int SwitchHash = Animator.StringToHash("Switch");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SwitchState()
    {
        _animator.SetTrigger(SwitchHash);
    }
}
