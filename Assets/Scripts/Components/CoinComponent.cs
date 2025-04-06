using Components;
using UnityEngine;

public class CoinComponent : MonoBehaviour
{
    [SerializeField] private int coinValue;

    public void PickupCoin(Player player)
    {
        player.PickCoin(coinValue);
    }
}
