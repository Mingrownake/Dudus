using System;
using UnityEngine;
using UnityEngine.Events;

public class EnterCollisionComponent : MonoBehaviour
{
    [SerializeField] private EnterEvent onEnter;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<Player>(out Player player))
        {
            onEnter?.Invoke(player);
        }
    }
    
    [Serializable]
    private class EnterEvent : UnityEvent<Player>
    {
        
    }
}
