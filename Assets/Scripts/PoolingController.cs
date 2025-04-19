using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingController : MonoBehaviour
{
    //[SerializeField] private StaticObjecObtacletPooling poolingA;
    //[SerializeField] private StaticObjecObtacletPooling poolingB;
    [SerializeField] private StaticObjectPooling myPooling;

    [SerializeField] private float fireRate = 1f;

    private float _timer = 0f;

    private void Start()
    {
        //StaticObjecObtacletPooling.SetUp(this.transform, poolingA);
        //StaticObjecObtacletPooling.SetUp(this.transform, poolingB);
        StaticObjectPooling.SetUp(this.transform, myPooling);
    }

    private void Update()
    {

        _timer += Time.deltaTime;

        if (_timer >= fireRate)
        {
            //string selectedPool = Random.value > 0.5f ? poolingA.PoolID : poolingB.PoolID;
            //GameObject obj = StaticObjecObtacletPooling.GetObject(selectedPool);
            //obj.transform.position = GetRandomSpawnPosition();
            GameObject objects = StaticObjectPooling.GetObject();

            objects.transform.position = GetRandomSpawnPosition();
            _timer = 0;
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float y = Random.Range(-5f, 5f); 
        return new Vector3(transform.position.x, y, 0);
    }
}

