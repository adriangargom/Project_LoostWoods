using UnityEngine;

public class DestructibleBox : MonoBehaviour
{
    private CameraShakeHandler _cameraShakeHandler; 
    private ItemsPoolManager _itemsPoolManager;

    void Start()
    {
        _cameraShakeHandler = CameraShakeHandler.Instance;
        _itemsPoolManager = ItemsPoolManager.Instance;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out WeaponSystem weaponSystem))
        {
            _cameraShakeHandler.LaunchCameraShake(2, .1f);

            GenerateReward();
            GenerateParticles();

            _itemsPoolManager.DestructibleItemsObjectPool.StoreItem(gameObject);
        }
    }

    private void GenerateReward()
    {
        int randomizeReward = Random.Range(0, 100);

        if(randomizeReward <= 10)
        {
            GameObject healthObject = ItemsPoolManager.Instance.HealthOrbPool.GetItem();
            healthObject.transform.position = transform.position;
        }
        else if (randomizeReward > 10 && randomizeReward <= 50)
        {
            GameObject goldObject = ItemsPoolManager.Instance.GoldPool.GetItem();
            goldObject.transform.position = transform.position;
        }
    }

    private void GenerateParticles()
    {
        GameObjectPool destroyParticlesPool = ParticlesPoolManager.Instance.DestroyParticlesPool;

        GameObject particles = destroyParticlesPool.GetItem();

        particles.transform.position = transform.position;
        particles.SetActive(true);

        ItemsPoolManager.Instance.LaunchDelayedItemStore(
            destroyParticlesPool,
            particles,
            5f
        );  
    }

}
