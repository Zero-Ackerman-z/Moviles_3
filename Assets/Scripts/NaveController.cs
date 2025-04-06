using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveController : MonoBehaviour
{
<<<<<<< Updated upstream
    // Referencias a los scripts de movimiento
=======
    public PlayerDataSO playerData;
    private PlayerLife playerLife;
>>>>>>> Stashed changes
    private AcceleratorMovement acceleratorMovement;
    private GyroscopeMovement gyroscopeMovement;

    [Header("Movement Settings")]
<<<<<<< Updated upstream
    public float velocidadMovimiento = 5f;    // Velocidad de movimiento en el eje Y
    public float limiteSuperior = 4.5f;       // Límite superior de movimiento
    public float limiteInferior = -4.5f;      // Límite inferior de movimiento

    private void Awake()
    {
        // Obtener las referencias a los scripts de movimiento
        acceleratorMovement = GetComponent<AcceleratorMovement>();
        gyroscopeMovement = GetComponent<GyroscopeMovement>();
=======
    public float velocidadMovimiento = 5f;    
    public float limiteSuperior = 4.5f;       
    public float limiteInferior = -4.5f;      

    private void Awake()
    {
        acceleratorMovement = GetComponent<AcceleratorMovement>();
        gyroscopeMovement = GetComponent<GyroscopeMovement>();
        playerLife = GetComponent<PlayerLife>();

>>>>>>> Stashed changes
    }

    private void Update()
    {
<<<<<<< Updated upstream
        // Controlar el movimiento de la nave según el sensor activado
=======
>>>>>>> Stashed changes
        if (acceleratorMovement.useAccelerator)
        {
            HandleAccelerometerMovement();
        }
        else if (gyroscopeMovement.useGyroscope)
        {
            HandleGyroscopeMovement();
        }
    }

<<<<<<< Updated upstream
    // Maneja el movimiento de la nave con el acelerómetro
    private void HandleAccelerometerMovement()
    {
        // Mover la nave en el eje Y basado en la aceleración
        float nuevaPosY = acceleratorMovement.GetVerticalMovement() * velocidadMovimiento * Time.deltaTime;
=======
    private void HandleAccelerometerMovement()
    {
        float nuevaPosY = acceleratorMovement.GetVerticalMovement() * playerData.MovementSpeed * Time.deltaTime;
>>>>>>> Stashed changes
        nuevaPosY = Mathf.Clamp(transform.position.y + nuevaPosY, limiteInferior, limiteSuperior);
        transform.position = new Vector3(transform.position.x, nuevaPosY, transform.position.z);
    }

<<<<<<< Updated upstream
    // Maneja el movimiento de la nave con el giroscopio
    private void HandleGyroscopeMovement()
    {
        // Mover la nave con el giroscopio
        float nuevaPosY = gyroscopeMovement.GetVerticalRotation() * velocidadMovimiento * Time.deltaTime;
        nuevaPosY = Mathf.Clamp(transform.position.y + nuevaPosY, limiteInferior, limiteSuperior);
        transform.position = new Vector3(transform.position.x, nuevaPosY, transform.position.z);
    }
=======
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
>>>>>>> Stashed changes
}
