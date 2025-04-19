using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private SimpleObjectPooling bulletPool;
    [SerializeField] private Transform bulletSpawnPoint;

    private PlayerDataSO playerData;
    private float shootTimer = 0f;
    private bool isHolding = false;

    private int _countBullets = 0;

    private void Awake()
    {
        bulletPool.SetUp(this.transform);
        bulletPool.onEnableObject += PrintBulletCount;
    }

    private void OnDisable()
    {
        bulletPool.onEnableObject -= PrintBulletCount;
    }

    private void Update()
    {
        isHolding = Input.touchCount > 0; 

        shootTimer += Time.deltaTime;

        if (isHolding && shootTimer >= playerData.Cadencia)
        {
            Disparar();
            shootTimer = 0f;
        }
    }

    private void Disparar()
    {
        GameObject bala = bulletPool.GetObject();
        if (bala != null)
        {
            bala.transform.position = bulletSpawnPoint.position;
            bala.transform.rotation = Quaternion.identity;

            Bullet bulletScript = bala.GetComponent<Bullet>();
            
        }
    }

    private void PrintBulletCount()
    {
        _countBullets++;
        Debug.Log(gameObject.name + " disparó: " + _countBullets + " balas");
    }

    public void AsignarDatos(PlayerDataSO data)
    {
        playerData = data;
    }
}
