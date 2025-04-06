using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreFinalText;
    [SerializeField] private TextMeshProUGUI naveNombreText;

    public ScoreSO scoreData;
    public GameObject naveJugador;           
    public GameObject enemySpawner;          
    public GameObject scoreManager;
    public GameObject panelResultados;
    public GameObject panelSelector;

    public void IniciarJuego(PlayerDataSO playerData)
    {
        naveJugador.SetActive(true);
        enemySpawner.SetActive(true);
        scoreManager.SetActive(true);
        if (naveJugador.TryGetComponent(out NaveController controller))
        {
            controller.playerData = playerData;
            controller.ActualizarVisual();
        }

        // Vida
        if (naveJugador.TryGetComponent(out PlayerLife vida))
        {
            vida.AsignarDatos(playerData);
            vida.OnGameOver -= TerminarJuego;
            vida.OnGameOver += TerminarJuego; 
        }

        // Score
        if (scoreManager.TryGetComponent(out ScoreManager score))
            score.AsignarDatos(playerData);
    }
    public void MostrarResultados(ScoreSO scoreData, string nombreNave)
    {
        if (naveNombreText != null)
            naveNombreText.text = "Nave usada: " + nombreNave;

        if (scoreFinalText != null)
            scoreFinalText.text = "Puntaje final: " + scoreData.puntuacion;
    }
    public void ReiniciarJuego()
    {
        panelSelector.SetActive(true);
        naveJugador.SetActive(false);
        enemySpawner.SetActive(false);
        scoreManager.SetActive(false);
        panelResultados.SetActive(false);

    }
    private IEnumerator EsperarYReiniciar(float segundos)
    {
        yield return new WaitForSecondsRealtime(segundos); 
        Time.timeScale = 1f; 

        ReiniciarJuego();
    }

    public void TerminarJuego()
    {
        Debug.Log("El juego ha terminado. Mostrando resultados...");
        Time.timeScale = 0f; 
        naveJugador.SetActive(false);
        enemySpawner.SetActive(false);
        scoreManager.SetActive(false);
        panelResultados.SetActive(true);
        if (naveJugador.TryGetComponent(out NaveController controller))
        {
            MostrarResultados(scoreData, controller.playerData.naveName);
        }
        StartCoroutine(EsperarYReiniciar(5f));

    }

}
