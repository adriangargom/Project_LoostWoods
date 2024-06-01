using System.Collections;
using UnityEngine;

public class ParticlesPoolManager : MonoBehaviour
{
    public static ParticlesPoolManager Instance { get; private set; }

    [Header("Hit Particles Pool")]
    [SerializeField] private GameObject _hitParticlesPoolPrefab;
    [SerializeField] private int _hitParticlesPoolQuantity;
    public GameObjectPool HitParticlesPool { get; private set; }

    [Header("Enemy Die Particles Pool")]
    [SerializeField] private GameObject _enemyDieParticlesPoolPrefab;
    [SerializeField] private int _enemyDieParticlesPoolQuantity;
    public GameObjectPool EnemyDieParticlesPool { get; private set; }

    [Header("Player Die Particles Pool")]
    [SerializeField] private GameObject _playerDieParticlesPoolPrefab;
    [SerializeField] private int _playerDieParticlesPoolQuantity;
    public GameObjectPool PlayerDieParticlesPool { get; private set; }

    [Header("Break Particles Pool")]
    [SerializeField] private GameObject _destroyParticlesPoolPrefab;
    [SerializeField] private int _destroyParticlesPoolQuantity;
    public GameObjectPool DestroyParticlesPool { get; private set; }

    [Header("Projectile Destroy Particles Pool")]
    [SerializeField] private GameObject _projectileDestroyParticlesPoolPrefab;
    [SerializeField] private int _projectileDestroyParticlesPoolQuantity;
    public GameObjectPool ProjectileDestroyParticlesPool { get; private set; }



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
        HitParticlesPool = new GameObjectPool(
            _hitParticlesPoolPrefab,
            _hitParticlesPoolQuantity,
            transform
        );

        EnemyDieParticlesPool = new GameObjectPool(
            _enemyDieParticlesPoolPrefab,
            _enemyDieParticlesPoolQuantity,
            transform
        );

        PlayerDieParticlesPool = new GameObjectPool(
            _playerDieParticlesPoolPrefab,
            _playerDieParticlesPoolQuantity,
            transform
        );

        DestroyParticlesPool = new GameObjectPool(
            _destroyParticlesPoolPrefab,
            _destroyParticlesPoolQuantity,
            transform
        );

        ProjectileDestroyParticlesPool = new GameObjectPool(
            _projectileDestroyParticlesPoolPrefab,
            _projectileDestroyParticlesPoolQuantity,
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