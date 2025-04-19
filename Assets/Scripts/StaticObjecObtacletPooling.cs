using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectPooling", menuName = "ScriptableObjects/StaticObjecObtacletPooling", order = 0)]
public class StaticObjecObtacletPooling : ScriptableObject
{
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private string poolID;

    private static Dictionary<string, Queue<GameObject>> poolDictionary = new();
    private static Dictionary<string, GameObject> prefabDictionary = new();
    private static Dictionary<string, Transform> parentDictionary = new();

    public string PoolID => poolID;

    public static void SetUp(Transform parent, StaticObjecObtacletPooling poolingInstance)
    {
        if (string.IsNullOrEmpty(poolingInstance.poolID))
        {
            Debug.LogError($"[StaticObjectPooling] poolID está vacío para: {poolingInstance.name}");
            return;
        }

        string id = poolingInstance.poolID;

        if (!poolDictionary.ContainsKey(id))
        {
            poolDictionary[id] = new Queue<GameObject>();
            prefabDictionary[id] = poolingInstance.objectPrefab;
            parentDictionary[id] = parent;
        }
    }

    public static GameObject GetObject(string poolID)
    {
        if (!poolDictionary.ContainsKey(poolID))
        {
            Debug.LogError($"[StaticObjectPooling] No pool found for ID: {poolID}");
            return null;
        }

        Queue<GameObject> pool = poolDictionary[poolID];
        GameObject prefab = prefabDictionary[poolID];
        Transform parent = parentDictionary[poolID];

        GameObject obj;

        if (pool.Count > 0)
        {
            obj = pool.Dequeue();
            obj.SetActive(true);
        }
        else
        {
            obj = GameObject.Instantiate(prefab, parent.position, Quaternion.identity);
            obj.transform.SetParent(parent);
        }

        return obj;
    }

    public static void ReturnObject(string poolID, GameObject obj)
    {
        if (!poolDictionary.ContainsKey(poolID))
        {
            Debug.LogError($"[StaticObjectPooling] No pool found for ID: {poolID}");
            return;
        }

        obj.SetActive(false);
        obj.transform.SetParent(parentDictionary[poolID]);
        obj.transform.position = parentDictionary[poolID].position;

        poolDictionary[poolID].Enqueue(obj);
    }
}
