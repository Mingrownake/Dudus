using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    public int Coins { get; private set; }

    public void CollectCoins(int amount)
    {
        Coins += amount;
        Debug.Log(Coins);
    }

    public void RemoveCoins(int amount)
    {
        Coins -= amount;
        Debug.Log(Coins);
    }
}
