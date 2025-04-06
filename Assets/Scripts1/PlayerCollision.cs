using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerLife playerLife;

    private void Awake()
    {
        playerLife = GetComponent<PlayerLife>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Obstacle" || other.tag == "Enemy" || other.tag == "Projectile")
        {
            playerLife.GetDamage(1);
        }
    }
}