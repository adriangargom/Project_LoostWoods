using System.Collections;
using UnityEngine;

public class EnemiesPoolManager : MonoBehaviour
{
    public static EnemiesPoolManager Instance { get; private set; }

    [Header("Base Enemies Pool")]
    [SerializeField] private GameObject[] _enemyPoolPrefabs;
    [SerializeField] private int _enemiesPoolQuantity;
    public EnemyObjectPool EnemyObjectPool { get; private set; }
    
    [Header("Boss Enemies Pool")]
    [SerializeField] private GameObject[] _bossPoolPrefabs;
    [SerializeField] private int _bossPoolQuantity;
    public EnemyObjectPool BossObjectPool { get; private set; }


    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        PreparePools();
    }

    private void PreparePools()
    {
        EnemyObjectPool = new EnemyObjectPool(
            _enemyPoolPrefabs,
            _enemiesPoolQuantity,
            transform
        );

        BossObjectPool = new EnemyObjectPool(
            _bossPoolPrefabs,
            _bossPoolQuantity,
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