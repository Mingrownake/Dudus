using System;
using UnityEngine;
using UnityEngine.Events;

public class EnterTriggerComponent : MonoBehaviour
{
    [SerializeField] private Collider2D _collider;
    [SerializeField] private TriggerEvent entered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            entered?.Invoke(player);
        }
    }

    [Serializable]
    private class TriggerEvent : UnityEvent<Player>
    {
        
    }
    
}
