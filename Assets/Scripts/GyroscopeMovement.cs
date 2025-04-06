using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroscopeMovement : MonoBehaviour
{
    [Header("Gyroscope Settings")]
    public bool useGyroscope = true;  // Si se debe usar el giroscopio
    public float giroscopioSensibilidad = 0.5f; // Sensibilidad para suavizar la rotación
    private Gyroscope gyroscope;

    private void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyroscope = Input.gyro;
            gyroscope.enabled = true; // Asegúrate de que el giroscopio está habilitado
            Debug.Log("Giroscopio activado");
        }
        else
        {
            Debug.LogWarning("El giroscopio no está disponible en este dispositivo");
        }
    }

    // Obtiene el movimiento vertical basado en la rotación del giroscopio
    public float GetVerticalRotation()
    {
        if (useGyroscope && gyroscope != null)
        {
            // Se obtiene la rotación del giroscopio
            Quaternion attitude = gyroscope.attitude;
            Vector3 eulerAngles = attitude.eulerAngles;

            // Normalizar el valor de la rotación para que vaya de -1 a 1
            float rotacionY = (eulerAngles.x - 180f) / 180f; // Mapea de 0-360 a -1 a 1

            // Aplicamos la sensibilidad para suavizar el movimiento
            rotacionY *= giroscopioSensibilidad;

            // Mostrar en el Debug la rotación en el eje X
            Debug.Log("Giroscopio - Rotación Eje X: " + rotacionY);

            return rotacionY;  // Eje X de la rotación (controlamos el movimiento vertical)
        }
        return 0f;
    }
}
