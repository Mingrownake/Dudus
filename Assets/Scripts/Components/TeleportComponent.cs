using UnityEngine;

public class TeleportComponent : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float timeBetweenTeleports;

    public void Teleport(Player player)
    {
        player.transform.position = target.position;
    } 
}
