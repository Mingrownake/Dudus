using UnityEngine;

public class DestroyObjectComponent : MonoBehaviour
{
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
