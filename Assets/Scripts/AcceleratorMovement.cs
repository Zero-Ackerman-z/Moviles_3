using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceleratorMovement : MonoBehaviour
{
    [Header("Accelerometer Settings")]
<<<<<<< Updated upstream
    public bool useAccelerator = true;  // Si se debe usar el aceler�metro
    public float acelerometroSensibilidad = 1.0f; // Sensibilidad para suavizar el movimiento

    // Obtiene el movimiento vertical a partir del aceler�metro
=======
    public bool useAccelerator = true;  
    public float acelerometroSensibilidad = 1.0f; 

>>>>>>> Stashed changes
    public float GetVerticalMovement()
    {
        if (useAccelerator)
        {
<<<<<<< Updated upstream
            // Obtiene el valor del aceler�metro en el eje Y
            float aceleracionY = Input.acceleration.y;

            // Aplicamos la sensibilidad para suavizar el movimiento
            aceleracionY *= acelerometroSensibilidad;

            // Mostrar en el Debug la aceleraci�n en el eje Y
            Debug.Log("Aceler�metro - Aceleraci�n Y: " + aceleracionY);

            return aceleracionY;  // Retorna la aceleraci�n en el eje Y
        }
        return 0f;  // Si no se usa, no mueve la nave
=======
            float aceleracionY = Input.acceleration.y;

            aceleracionY *= acelerometroSensibilidad;

            Debug.Log("Aceler�metro - Aceleraci�n Y: " + aceleracionY);

            return aceleracionY;  
        }
        return 0f;  
>>>>>>> Stashed changes
    }
}
