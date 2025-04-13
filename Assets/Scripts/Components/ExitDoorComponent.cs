using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoorComponent : MonoBehaviour
{
    [SerializeField] private string _nameLevel;
    
    public void ExitLevel()
    {
        if (_nameLevel == "") return;
        SceneManager.LoadScene(_nameLevel);
        
    }
}
