using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceleratorMovement : MonoBehaviour
{
    [Header("Accelerometer Settings")]
    public bool useAccelerator = true;  
    public float acelerometroSensibilidad = 1.0f; 

    public float GetVerticalMovement()
    {
        if (useAccelerator)
        {
            float aceleracionY = Input.acceleration.y;

            aceleracionY *= acelerometroSensibilidad;

            Debug.Log("Acelerómetro - Aceleración Y: " + aceleracionY);

            return aceleracionY;  
        }
        return 0f;  
    }
}
