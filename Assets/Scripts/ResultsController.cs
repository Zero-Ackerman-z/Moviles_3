using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{

    public class ResultsController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreFinalText;
        [SerializeField] private TextMeshProUGUI naveNombreText;
        [SerializeField] private TextMeshProUGUI highScoreText;

        public void ConfigurarResultados(ScoreSO scoreData, string nombreNave)
        {
            if (naveNombreText != null)
                naveNombreText.text = "Nave usada: " + nombreNave;

            if (scoreFinalText != null)
                scoreFinalText.text = "Puntaje final: " + scoreData.puntuacion;

            if (highScoreText != null)
            {
                int highScore = PlayerPrefs.GetInt("HighScore", 0);
                highScoreText.text = "Mejor puntaje: " + highScore;
            }
            StartCoroutine(EsperarYReiniciar(5f)); 

        }
        private IEnumerator EsperarYReiniciar(float segundos)
        {
            yield return new WaitForSecondsRealtime(segundos);
            Time.timeScale = 1f;
            SceneGlobalManager.Instance.VolverAMenu();
        }
    }
}
