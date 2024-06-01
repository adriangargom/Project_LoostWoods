using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool: IPoolConsumer<GameObject>
{
    private readonly StaticPool<GameObject> _itemsPool;

    public GameObjectPool(GameObject poolItem, int poolSize, Transform parent)
    {
        List<GameObject> instances = ObjectInstancer.InstantiateObjects(
            poolItem,
            poolSize,
            parent
        );

        _itemsPool = new StaticPool<GameObject>(instances);
    }

    public GameObjectPool(GameObject[] poolItems, int poolSize, Transform parent)
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