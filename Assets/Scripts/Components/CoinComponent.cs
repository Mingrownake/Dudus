using UnityEngine;

public class CoinComponent : MonoBehaviour
{
    [SerializeField] private int coinValue;

    public void PickupCoin()
    {
        Debug.Log($"Coin {coinValue}");
    }
}
