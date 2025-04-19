using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRigidbody;
    [SerializeField] private float speed;
    private void OnEnable()
    {
        if (myRigidbody == null)
            myRigidbody = GetComponent<Rigidbody2D>();

        myRigidbody.velocity = Vector2.left * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Wall" || collision.tag == "Bullet")
        {

            if (collision.tag == "Bullet")
            {
                EventManager.PuntajeGanado(10); // Por ejemplo, 10 puntos por obstáculo

            }

            StaticObjectPooling.ObjectReturn(gameObject);


        }
    }
}