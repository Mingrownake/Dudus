using Stats.Models;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    public PlayerData Data => playerData;
    
    private void Awake()
    {
        if (StateIsExit())
        {
            DestroyImmediate(this);
        }
        else
        {
            DontDestroyOnLoad(this);
        }
    }

    private bool StateIsExit()
    {
        var managers = FindObjectsByType<StateManager>(FindObjectsSortMode.None);
        for (int i = 0; i < managers.Length; i++)
        {
            if (managers[i] != this)
            {
                return true;
            }
        }
        return false;
    }
}
