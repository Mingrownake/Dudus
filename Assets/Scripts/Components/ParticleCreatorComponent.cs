using UnityEngine;

public class ParticleCreatorComponent : MonoBehaviour
{
    [Header("Run Particles")]
    [SerializeField] private Transform _targetRun;
    [SerializeField] private GameObject particleRun;

    [Header("Jump Particles")]
    [SerializeField] private Transform _targetJumpPoint;
    [SerializeField] private GameObject particleJump;
    
    [Header("Fall Particles")]
    [SerializeField] private Transform _targetFallPoint;
    [SerializeField] private GameObject particleFall;
    
    [Header("Fall Coins")]
    [SerializeField] private Transform _targetCoinsFallPoint;
    [SerializeField] private GameObject particleCoinsFall;

    public void SpawnRunParticles()
    {
        GameObject particle = Instantiate(particleRun, _targetRun);
        particle.transform.SetParent(null);
        particle.transform.eulerAngles = _targetRun.lossyScale;
    }

    public void SpawnJumpParticles()
    {
        GameObject particle = Instantiate(particleJump, _targetJumpPoint);
        particle.transform.SetParent(null);
    }

    public void SpawnFallParticles()
    {
        GameObject particle = Instantiate(particleFall, _targetFallPoint);
        particle.transform.SetParent(null);
    }

    public ParticleSystem SpawnCoinsFallParticles()
    {
        GameObject particle = Instantiate(particleCoinsFall, _targetCoinsFallPoint);
        particle.transform.SetParent(null);
        return particle.GetComponent<ParticleSystem>();
    }
}
