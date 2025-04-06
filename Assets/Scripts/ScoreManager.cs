using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("Score Settings")]
    public float puntosPorSegundo = 10f;  // Cuántos puntos se suman por segundo
    private float puntos = 0f;  // Puntos acumulados

    [Header("UI Settings")]
    public TextMeshProUGUI scoreText;  // Referencia al texto de puntuación en la UI

    private void Start()
    {
        if (scoreText == null)
        {
            Debug.LogError("No se ha asignado el texto de puntuación en la UI.");
        }
    }

    private void Update()
    {
        // Aumentar los puntos con el tiempo
        puntos += puntosPorSegundo * Time.deltaTime;

        // Actualizar el texto con los puntos
        UpdateScoreText();
    }

    // Actualiza el texto de la puntuación en la UI
    private void UpdateScoreText()
    {
        // Muestra los puntos con formato (por ejemplo, 2 decimales)
        scoreText.text = "Puntos: " + puntos.ToString("F0");  // "F0" muestra sin decimales
    }
}
