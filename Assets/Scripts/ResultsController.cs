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
        private bool yaSaliendo = false;
        private float tiempoPresionado = 1f;
        private float duracionParaSalir = 1f;
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
            StartCoroutine(EsperarYReiniciar(10f)); 

        }
        private IEnumerator EsperarYReiniciar(float segundos)
        {
            yield return new WaitForSecondsRealtime(segundos);
            Time.timeScale = 1f;
            SceneGlobalManager.Instance.IrASeleccionDeNave();
        }
        void Update()
        {
            Debug.Log("Saliendo al MENU por input");
            if (yaSaliendo) return;

            if (Input.GetMouseButton(0))
            {
                tiempoPresionado += Time.unscaledDeltaTime;

                if (tiempoPresionado >= duracionParaSalir)
                {
                    SalirAlMenu();
                }
            }
            else
            {
                tiempoPresionado = 0f; 
            }
        }

        private void SalirAlMenu()
        {
            yaSaliendo = true;
            Time.timeScale = 1f;
            SceneGlobalManager.Instance.VolverAMenu();
        }
    }
}
