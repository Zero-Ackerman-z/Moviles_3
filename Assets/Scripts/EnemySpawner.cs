using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  
    public float spawnInterval = 2f; 
    public float spawnYMin = -4f;  
    public float spawnYMax = 4f;    
    public Transform spawnPoint;   

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        float spawnPosY = Random.Range(spawnYMin, spawnYMax);
        Vector3 spawnPosition = new Vector3(spawnPoint.position.x, spawnPosY, spawnPoint.position.z);

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
