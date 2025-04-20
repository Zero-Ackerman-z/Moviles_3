using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

namespace Assets.Scripts
{
    public class SceneGlobalManager : MonoBehaviour
    {
        public static SceneGlobalManager Instance;

        private string currentGameScene = "Game";
        private string resultsScene = "Results";
        private string menuScene = "Menu";
        private string characterSelectScene = "CharacterSelect";

        // Definir eventos
        public event Action OnSceneChanged;
        public event Action<PlayerDataSO> OnGameStarted;
        public event Action OnGameEnded;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            StartCoroutine(CargarMenuDesdeSplash());
        }

        private IEnumerator CargarMenuDesdeSplash()
        {
            yield return new WaitForSeconds(2f);
            SceneManager.LoadSceneAsync(menuScene, LoadSceneMode.Single);
            OnSceneChanged?.Invoke();  // Invocar evento de cambio de escena
        }

        public void CargarEscenaGameConResults(PlayerDataSO naveSeleccionada)
        {
            StartCoroutine(CargarEscenasDeJuego(naveSeleccionada));
        }

        private IEnumerator CargarEscenasDeJuego(PlayerDataSO naveSeleccionada)
        {
            if (SceneManager.GetSceneByName(characterSelectScene).isLoaded)
                yield return SceneManager.UnloadSceneAsync(characterSelectScene);

            AsyncOperation gameLoad = SceneManager.LoadSceneAsync(currentGameScene, LoadSceneMode.Additive);
            AsyncOperation resultsLoad = SceneManager.LoadSceneAsync(resultsScene, LoadSceneMode.Additive);

            while (!gameLoad.isDone || !resultsLoad.isDone)
                yield return null;

            GameManager gm = FindObjectOfType<GameManager>();
            if (gm != null)
            {
                gm.IniciarJuego(naveSeleccionada);
            }

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentGameScene));
            OnGameStarted?.Invoke(naveSeleccionada);
        }

        public void MostrarResultados(ScoreSO scoreData, string naveName)
        {
            Scene resultsSceneObj = SceneManager.GetSceneByName(resultsScene);
            if (resultsSceneObj.IsValid())
            {
                foreach (GameObject root in resultsSceneObj.GetRootGameObjects())
                {
                    ResultsController controller = root.GetComponentInChildren<ResultsController>();
                    if (controller != null)
                    {
                        controller.ConfigurarResultados(scoreData, naveName);
                        break;
                    }
                }
            }
            OnGameEnded?.Invoke();  // Invocar evento de fin del juego
        }

        public void VolverAMenu()
        {
            StartCoroutine(UnloadYVolver(menuScene));
        }

        public void IrASeleccionDeNave()
        {
            StartCoroutine(UnloadYVolver(characterSelectScene));
        }

        private IEnumerator UnloadYVolver(string escenaDestino)
        {
            if (SceneManager.GetSceneByName(currentGameScene).isLoaded)
                yield return SceneManager.UnloadSceneAsync(currentGameScene);

            if (SceneManager.GetSceneByName(resultsScene).isLoaded)
                yield return SceneManager.UnloadSceneAsync(resultsScene);

            yield return SceneManager.LoadSceneAsync(escenaDestino, LoadSceneMode.Single);
            OnSceneChanged?.Invoke();  // Invocar evento de cambio de escena
        }

        public void LoadSceneAdditiveAsync(string sceneName, Action callback = null)
        {
            StartCoroutine(LoadSceneCoroutine(sceneName, callback));
        }
        private IEnumerator LoadSceneCoroutine(string sceneName, Action callback)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            while (!asyncOperation.isDone)
            {
                yield return null;
            }

            callback?.Invoke();
        }

    }

}