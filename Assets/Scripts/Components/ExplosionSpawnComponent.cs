using UnityEngine;

public class ExplosionSpawnComponent : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private GameObject explosionParticle;

    public void Spawn()
    {
        GameObject particle = Instantiate(explosionParticle, _target);
        particle.transform.SetParent(null);
    }
}
