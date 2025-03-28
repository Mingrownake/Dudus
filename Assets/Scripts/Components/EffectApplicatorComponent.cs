using Components;
using UnityEngine;

public class EffectApplicatorComponent : MonoBehaviour
{
    [SerializeField] private int effectValue;
    [SerializeField] private EffectType effectType;

    public void ApplyEffect(Player player)
    {
        player.ApplyEffect(effectValue, effectType);
    }
}

