using System.Linq;
using UnityEngine;

public class ProjectileCollisionSystem : MonoBehaviour
{
    [SerializeField] private string[] _excludedTags;

    void OnTriggerEnter(Collider other)
    {
        if(_excludedTags.Contains(other.tag)) return;

        DestroyParticles();
        gameObject.SetActive(false);
    }

    private void DestroyParticles()
    {
        ParticlesPoolManager particlesPoolManager = ParticlesPoolManager.Instance;

        GameObject dieParticles = particlesPoolManager.ProjectileDestroyParticlesPool.GetItem();
        dieParticles.transform.position = transform.position;
        
        particlesPoolManager.LaunchDelayedItemStore(
            particlesPoolManager.ProjectileDestroyParticlesPool,
            dieParticles,
            5f
        );
    }
}
