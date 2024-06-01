using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : IPoolConsumer<GameObject>
{
    public readonly StaticPool<GameObject> _itemsPool;


    public EnemyObjectPool(GameObject[] poolItems, int poolSize, Transform parent)
    {
        List<GameObject> instances = ObjectInstancer.InstantiateObjects(
            poolItems,
            poolSize,
            parent
        );

        _itemsPool = new StaticPool<GameObject>(instances);
    }

    
    public GameObject GetItem()
    {
        GameObject item = _itemsPool.GetObject();
        item.SetActive(true);

        return item;
    }

    public void StoreItem(GameObject item)
    {
        if(item.TryGetComponent(out EnemyController enemyController))
        {
            enemyController.HealthSystem.SetActualHealth(
                enemyController.StatsManager.ActualStats[StatsEnum.MaxHealth]
            );

            enemyController.FiniteStateMachine.ChangeToDefaultState();
        }

        item.SetActive(false);
        _itemsPool.ReleaseObject(item);
    }

    public void ReleaseFullPool()
    {
        List<GameObject> tmpCopy = new(_itemsPool.InUseItems);

        foreach (GameObject item in tmpCopy)
        {
            StoreItem(item);
        }
    }
}