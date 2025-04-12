
using System.Collections.Generic;
using UnityEngine;

public class CheckCircleOverlap : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float _radius = 1f;
    
    public HealthComponent[] CheckCircles()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, _radius);
        List<HealthComponent> result = new List<HealthComponent>();
        for (int i = 0; i < colliders.Length; i++)
        {
            var collider = colliders[i];
            if (!collider.TryGetComponent(out Player p) &&
                collider.TryGetComponent<HealthComponent>(out HealthComponent healthComponent))
            {
                result.Add(healthComponent);
            } 
        }
        return result.ToArray();
    }
}
