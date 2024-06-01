using System.Collections;
using UnityEngine;

public class ItemsPoolManager : MonoBehaviour
{
    public static ItemsPoolManager Instance { get; private set; }

    [Header("Gold Pool")]
    [SerializeField] private GameObject _goldPoolPrefab;
    [SerializeField] private int _goldPoolQuantity;
    public GameObjectPool GoldPool { get; private set; }

    [Header("Health Orb Pool")]
    [SerializeField] private GameObject _healthOrbPoolPrefab;
    [SerializeField] private int _healthOrbPoolQuantity;
    public GameObjectPool HealthOrbPool { get; private set; }

    [Header("Arrow Projectiles Pool")]
    [SerializeField] private GameObject _arrowProjectilePrefab;
    [SerializeField] private int _arrowProjectilesPoolQuantity;
    public GameObjectPool ArrowProjectilesPool { get; private set; }

    [Header("Arrow Projectiles Pool")]
    [SerializeField] private GameObject _spikeArrowProjectilePrefab;
    [SerializeField] private int __spikeArrowProjectilesPoolQuantity;
    public GameObjectPool SpikeArrowProjectilesPool { get; private set; }

    [Header("Plant Projectiles Pool")]
    [SerializeField] private GameObject _plantProjectilePrefab;
    [SerializeField] private int _plantProjectilesPoolQuantity;
    public GameObjectPool PlantProjectilesPool { get; private set; }


    [Header("Destructible Items Pool")]
    [SerializeField] private GameObject[] _destructibleItemsPoolPrefabs;
    [SerializeField] private int _destructibleItemsPoolQuantity;
    public GameObjectPool DestructibleItemsObjectPool { get; private set; }



    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        InitializePools();
    }

    private void InitializePools()
    {
        GoldPool = new GameObjectPool(
            _goldPoolPrefab,
            _goldPoolQuantity,
            transform
        );

        HealthOrbPool = new GameObjectPool(
            _healthOrbPoolPrefab,
            _healthOrbPoolQuantity,
            transform
        );

        ArrowProjectilesPool = new GameObjectPool(
            _arrowProjectilePrefab,
            _arrowProjectilesPoolQuantity,
            transform
        );

        SpikeArrowProjectilesPool = new GameObjectPool(
            _spikeArrowProjectilePrefab,
            __spikeArrowProjectilesPoolQuantity,
            transform
        );
    
        PlantProjectilesPool = new GameObjectPool(
            _plantProjectilePrefab,
            _plantProjectilesPoolQuantity,
            transform
        );

        DestructibleItemsObjectPool = new GameObjectPool(
            _destructibleItemsPoolPrefabs,
            _destructibleItemsPoolQuantity,
            transform
        );
    }

    
    public void LaunchDelayedItemStore(GameObjectPool pool, GameObject item, float seconds) {
        StartCoroutine(DelayedItemStore(pool, item, seconds));
    }

    IEnumerator DelayedItemStore(GameObjectPool pool, GameObject item, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        pool.StoreItem(item);
    }
}