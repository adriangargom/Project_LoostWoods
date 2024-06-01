using System.Collections.Generic;
using UnityEngine;

public class ObjectInstancer: MonoBehaviour
{
    public static GameObject InstantiateObject(
        GameObject objectPrefab,
        Transform parent
    ) {
        GameObject instance = Instantiate(objectPrefab, Vector3.zero, objectPrefab.transform.rotation);
        instance.transform.parent = parent;
        instance.SetActive(false);

        return instance;
    }

    // Instantiates the provided object prefab by the provided quantity of times
    public static List<GameObject> InstantiateObjects(
        GameObject objectPrefab, 
        int quantity, 
        Transform parent
    ) {
        List<GameObject> instances = new();

        for (int i = 0; i < quantity; i++)
        {
            GameObject instance = InstantiateObject(objectPrefab, parent);
            instances.Add(instance);
        }

        return instances;
    }

    // Instantiates each one of the provided objects by the provided quantity of times
    public static List<GameObject> InstantiateObjects(
        GameObject[] objectPrefabs, 
        int quantity, 
        Transform parent
    ) {
        List<GameObject> instances = new();

        for (int i = 0; i < quantity; i++)
        {
            for (int j = 0; j < objectPrefabs.Length; j++)
            {
                GameObject instance = InstantiateObject(objectPrefabs[j], parent);
                instances.Add(instance);
            }
        }

        return instances;
    }
}