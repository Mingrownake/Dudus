using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    private StateManager _stateManager;
    public int Coins { get; private set; }

    public void CollectCoins(int amount)
    {
        Coins += amount;
        _stateManager.Data.money = Coins;
        Debug.Log(Coins);
    }

    public void RemoveCoins(int amount)
    {
        Coins -= amount;
        _stateManager.Data.money = Coins;
        Debug.Log(Coins);
    }

    public void InitStateManager(StateManager manager)
    {
        _stateManager = manager;
    }
}
