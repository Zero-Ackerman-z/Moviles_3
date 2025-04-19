using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public ScoreSO scoreData;  
    public PlayerDataSO playerData;
    public TextMeshProUGUI scoreText;
    private Coroutine scoreCoroutine;
    private void OnEnable()
    {
        EventManager.OnPuntajeGanado += AgregarPuntos;
    }

    private void OnDisable()
    {
        EventManager.OnPuntajeGanado -= AgregarPuntos;
    }

    public void AsignarDatos(PlayerDataSO data)
    {
        playerData = data;
        scoreData.ResetearPuntaje();

        if (scoreCoroutine != null)
            StopCoroutine(scoreCoroutine);

        scoreCoroutine = StartCoroutine(IncrementarPuntosProgresivamente());
    }
    private IEnumerator IncrementarPuntosProgresivamente()
    {
        while (true)
        {
            scoreData.AumentarPuntos(Mathf.RoundToInt(playerData.ScoringSpeed));
            scoreText.text = $"Puntos: {scoreData.puntuacion}";
            yield return new WaitForSeconds(1f); 
        }
    }
    public void AgregarPuntos(int puntos)
    {
        scoreData.AumentarPuntos(puntos);
        scoreText.text = $"Puntos: {scoreData.puntuacion}";
    }
    public void GuardarHighScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (scoreData.puntuacion > highScore)
        {
            PlayerPrefs.SetInt("HighScore", scoreData.puntuacion);
            PlayerPrefs.Save();
        }
    }

    public int ObtenerHighScore()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
    }

}


