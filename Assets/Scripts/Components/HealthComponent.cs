using Components;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private UnityEvent OnEffect;
    [SerializeField] private UnityEvent OnDeath;
    [SerializeField] private int health;

    public void ApplyEffect(int value, EffectType effectType)
    {
        switch (effectType)
        {
            case EffectType.Health:
                health += value;
                break;
            case EffectType.Damage:
                health -= value;
                break;
        }
        OnEffect?.Invoke();
        if (health <= 0)
        {
            OnDeath?.Invoke();
        }
    }
}
