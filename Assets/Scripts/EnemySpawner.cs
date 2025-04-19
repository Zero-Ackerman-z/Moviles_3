using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject[] ObjectsPrefab;
    //public GameObject enemyPrefab;
    public float spawnInterval; 
    public float spawnYMin = -4f;  
    public float spawnYMax = 4f;
    [Header("Spawn Point")]
    public Transform spawnPoint;   

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (ObjectsPrefab.Length == 0) return;
        float spawnPosY = Random.Range(spawnYMin, spawnYMax);
        Vector3 spawnPosition = new Vector3(spawnPoint.position.x, spawnPosY, spawnPoint.position.z);

        int randomIndex = Random.Range(0, ObjectsPrefab.Length);  // Elige un prefab al azar
        GameObject selectedEnemy = ObjectsPrefab[randomIndex];

        Instantiate(selectedEnemy, spawnPosition, Quaternion.identity);
    }
}
