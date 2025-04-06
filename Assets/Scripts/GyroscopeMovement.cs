using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroscopeMovement : MonoBehaviour
{
    [Header("Gyroscope Settings")]
    public bool useGyroscope = true;  // Si se debe usar el giroscopio
    public float giroscopioSensibilidad = 0.5f; // Sensibilidad para suavizar la rotaci�n
    private Gyroscope gyroscope;

    private void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyroscope = Input.gyro;
            gyroscope.enabled = true; // Aseg�rate de que el giroscopio est� habilitado
            Debug.Log("Giroscopio activado");
        }
        else
        {
            Debug.LogWarning("El giroscopio no est� disponible en este dispositivo");
        }
    }

    // Obtiene el movimiento vertical basado en la rotaci�n del giroscopio
    public float GetVerticalRotation()
    {
        if (useGyroscope && gyroscope != null)
        {
            // Se obtiene la rotaci�n del giroscopio
            Quaternion attitude = gyroscope.attitude;
            Vector3 eulerAngles = attitude.eulerAngles;

            // Normalizar el valor de la rotaci�n para que vaya de -1 a 1
            float rotacionY = (eulerAngles.x - 180f) / 180f; // Mapea de 0-360 a -1 a 1

            // Aplicamos la sensibilidad para suavizar el movimiento
            rotacionY *= giroscopioSensibilidad;

            // Mostrar en el Debug la rotaci�n en el eje X
            Debug.Log("Giroscopio - Rotaci�n Eje X: " + rotacionY);

            return rotacionY;  // Eje X de la rotaci�n (controlamos el movimiento vertical)
        }
        return 0f;
    }
}
