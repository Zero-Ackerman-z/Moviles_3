using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceleratorMovement : MonoBehaviour
{
    [Header("Accelerometer Settings")]
    public bool useAccelerator = true;  // Si se debe usar el acelerómetro
    public float acelerometroSensibilidad = 1.0f; // Sensibilidad para suavizar el movimiento

    // Obtiene el movimiento vertical a partir del acelerómetro
    public float GetVerticalMovement()
    {
        if (useAccelerator)
        {
            // Obtiene el valor del acelerómetro en el eje Y
            float aceleracionY = Input.acceleration.y;

            // Aplicamos la sensibilidad para suavizar el movimiento
            aceleracionY *= acelerometroSensibilidad;

            // Mostrar en el Debug la aceleración en el eje Y
            Debug.Log("Acelerómetro - Aceleración Y: " + aceleracionY);

            return aceleracionY;  // Retorna la aceleración en el eje Y
        }
        return 0f;  // Si no se usa, no mueve la nave
    }
}
