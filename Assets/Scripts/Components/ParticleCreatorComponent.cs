using UnityEngine;

public class ParticleCreatorComponent : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private GameObject particlePrefab;

    [ContextMenu("Spawn")]
    public void SpawnParticles()
    {
        GameObject particle = Instantiate(particlePrefab, _target);
        particle.transform.SetParent(null);
        particle.transform.eulerAngles = _target.lossyScale;
    }
}
