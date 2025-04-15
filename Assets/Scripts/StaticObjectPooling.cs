using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectPooling", menuName = "ScriptableObjects/StaticObjectPooling", order = 0)]
public class StaticObjectPooling : ScriptableObject
{
    [SerializeField] private GameObject objectPrefab; 
    private static GameObject objectPrefabStatic; 
    private static Queue<GameObject> objectPool = new Queue<GameObject>(); // Pool estático
    private static Transform parentTransform;

    public static event Action onEnableObject;

    public static void SetUp(Transform parent, StaticObjectPooling poolingInstance)
    {
        if (objectPool == null)
        {
            objectPool = new Queue<GameObject>();
        }

        objectPool.Clear();
        parentTransform = parent;

        objectPrefabStatic = poolingInstance.objectPrefab;
    }

    // Obtener objeto del pool
    public static GameObject GetObject()
    {
        GameObject objectInstance = null;

        if (objectPool.Count > 0)
        {
            objectInstance = objectPool.Dequeue();
            objectInstance.SetActive(true);
            onEnableObject?.Invoke();
        }
        else
        {
            if (parentTransform != null)
            {
                objectInstance = Instantiate(objectPrefabStatic, parentTransform.position, Quaternion.identity);
                objectInstance.transform.SetParent(parentTransform);
            }
            objectInstance.SetActive(true);
            onEnableObject?.Invoke();
        }

        return objectInstance;
    }

    // Devolver objeto al pool
    public static void ObjectReturn(GameObject objectInstance)
    {
        objectInstance.SetActive(false);
        objectInstance.transform.position = parentTransform.position;
        objectInstance.transform.SetParent(parentTransform); 
        objectPool.Enqueue(objectInstance);
    }
}
