using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyMovement : MonoBehaviour
{
    public float speed = 3f; 
    public float lifeTime = 5f;

    private void OnEnable()
    {
        CancelInvoke();
        Invoke(nameof(ReturnToPool), lifeTime);  // Devolvemos al pool después de 'lifeTime' segundos
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
    private void ReturnToPool()
    {
        StaticObjectPooling.ObjectReturn(gameObject);
    }
}
