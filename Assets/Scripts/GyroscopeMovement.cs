using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroscopeMovement : MonoBehaviour
{
    [Header("Gyroscope Settings")]
    public bool useGyroscope = true;  
    public float giroscopioSensibilidad = 0.5f;
    private Gyroscope gyroscope;

    private void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyroscope = Input.gyro;
            gyroscope.enabled = true; 
            Debug.Log("Giroscopio activado");
        }
        else
        {
            Debug.LogWarning("El giroscopio no está disponible en este dispositivo");
        }
    }

    public float GetVerticalRotation()
    {
        if (useGyroscope && gyroscope != null)
        {
            Quaternion attitude = gyroscope.attitude;
            Vector3 eulerAngles = attitude.eulerAngles;

            float rotacionY = (eulerAngles.x - 180f) / 180f; 

            rotacionY *= giroscopioSensibilidad;

            Debug.Log("Giroscopio - Rotación Eje X: " + rotacionY);

            return rotacionY;  
        }
        return 0f;
    }
}
