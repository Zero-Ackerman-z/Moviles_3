using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveController : MonoBehaviour
{
    public PlayerDataSO playerData;
    private PlayerLife playerLife;
    private AcceleratorMovement acceleratorMovement;
    private GyroscopeMovement gyroscopeMovement;

    [Header("Movement Settings")]
    public float velocidadMovimiento = 5f;    
    public float limiteSuperior = 4.5f;       
    public float limiteInferior = -4.5f;
    private SpriteRenderer spriteRenderer; 

    private void Awake()
    {
        acceleratorMovement = GetComponent<AcceleratorMovement>();
        gyroscopeMovement = GetComponent<GyroscopeMovement>();
        playerLife = GetComponent<PlayerLife>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("No se encontró un SpriteRenderer en los hijos.");
        }

    }
    public void ActualizarVisual()
    {
        if (playerData != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = playerData.naveSprite;
            Debug.Log("Sprite asignado correctamente: " + playerData.naveSprite.name);
        }
        else
        {
            Debug.LogError("PlayerData o SpriteRenderer es nulo en ActualizarVisual");
        }
    }

    private void Update()
    {
        if (acceleratorMovement.useAccelerator)
        {
            HandleAccelerometerMovement();
        }
        else if (gyroscopeMovement.useGyroscope)
        {
            HandleGyroscopeMovement();
        }
    }

    private void HandleAccelerometerMovement()
    {
        float nuevaPosY = acceleratorMovement.GetVerticalMovement() * playerData.MovementSpeed * Time.deltaTime;
        nuevaPosY = Mathf.Clamp(transform.position.y + nuevaPosY, limiteInferior, limiteSuperior);
        transform.position = new Vector3(transform.position.x, nuevaPosY, transform.position.z);
    }

    private void HandleGyroscopeMovement()
    {
        float nuevaPosY = gyroscopeMovement.GetVerticalRotation() * playerData.MovementSpeed * Time.deltaTime;
        nuevaPosY = Mathf.Clamp(transform.position.y + nuevaPosY, limiteInferior, limiteSuperior);
        transform.position = new Vector3(transform.position.x, nuevaPosY, transform.position.z);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Obstacle" || other.tag == "Enemy" || other.tag == "Projectile")
        {
            playerLife.GetDamage(1);
        }
    }
}
