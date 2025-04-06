using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveController : MonoBehaviour
{
    // Referencias a los scripts de movimiento
    private AcceleratorMovement acceleratorMovement;
    private GyroscopeMovement gyroscopeMovement;

    [Header("Movement Settings")]
    public float velocidadMovimiento = 5f;    // Velocidad de movimiento en el eje Y
    public float limiteSuperior = 4.5f;       // L�mite superior de movimiento
    public float limiteInferior = -4.5f;      // L�mite inferior de movimiento

    private void Awake()
    {
        // Obtener las referencias a los scripts de movimiento
        acceleratorMovement = GetComponent<AcceleratorMovement>();
        gyroscopeMovement = GetComponent<GyroscopeMovement>();
    }

    private void Update()
    {
        // Controlar el movimiento de la nave seg�n el sensor activado
        if (acceleratorMovement.useAccelerator)
        {
            HandleAccelerometerMovement();
        }
        else if (gyroscopeMovement.useGyroscope)
        {
            HandleGyroscopeMovement();
        }
    }

    // Maneja el movimiento de la nave con el aceler�metro
    private void HandleAccelerometerMovement()
    {
        // Mover la nave en el eje Y basado en la aceleraci�n
        float nuevaPosY = acceleratorMovement.GetVerticalMovement() * velocidadMovimiento * Time.deltaTime;
        nuevaPosY = Mathf.Clamp(transform.position.y + nuevaPosY, limiteInferior, limiteSuperior);
        transform.position = new Vector3(transform.position.x, nuevaPosY, transform.position.z);
    }

    // Maneja el movimiento de la nave con el giroscopio
    private void HandleGyroscopeMovement()
    {
        // Mover la nave con el giroscopio
        float nuevaPosY = gyroscopeMovement.GetVerticalRotation() * velocidadMovimiento * Time.deltaTime;
        nuevaPosY = Mathf.Clamp(transform.position.y + nuevaPosY, limiteInferior, limiteSuperior);
        transform.position = new Vector3(transform.position.x, nuevaPosY, transform.position.z);
    }
}
