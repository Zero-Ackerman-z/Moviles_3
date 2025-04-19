using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private SimpleObjectPooling myPool;

    [SerializeField] private Rigidbody2D myRigidbody;
    [SerializeField] private float speed = 10f;

    private void OnEnable()
    {
        if (myRigidbody == null)
            myRigidbody = GetComponent<Rigidbody2D>();

        myRigidbody.velocity = Vector2.right * speed;
    }
    public void SetPool(SimpleObjectPooling pool)
    {
        myPool = pool;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle") || collision.CompareTag("Wall"))
        {
            myPool.ObjectReturn(gameObject); 
        }
    }
}
