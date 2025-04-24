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
            EventManager.InvokeSceneChanged();
        }

        public void CargarEscenaGameConResults(PlayerDataSO naveSeleccionada)
        {
            StartCoroutine(CargarEscenasDeJuego(naveSeleccionada));
        }




        private IEnumerator CargarEscenasDeJuego(PlayerDataSO naveSeleccionada)
        {
            // 1. Limpiar escenas anteriores
            yield return LimpiarEscenasAnteriores();

            // 2. Cargar Game como SINGLE (principal)
            yield return SceneManager.LoadSceneAsync(currentGameScene, LoadSceneMode.Single);

            // 3. Cargar Results como ADITIVA
            yield return SceneManager.LoadSceneAsync(resultsScene, LoadSceneMode.Additive);

            // 4. Ocultar Results inicialmente
            Scene resultsSceneObj = SceneManager.GetSceneByName(resultsScene);
            SetActiveAllObjects(resultsSceneObj, false);

            // 5. Configurar y activar Game
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentGameScene));

            GameManager gm = FindObjectOfType<GameManager>();
            if (gm != null)
            {
                gm.IniciarJuego(naveSeleccionada);
            }

            EventManager.InvokeGameStarted(naveSeleccionada);
        }

        // Método auxiliar para activar/desactivar todos los objetos de una escena
        private void SetActiveAllObjects(Scene scene, bool state)
        {
            if (!scene.IsValid()) return;

            foreach (GameObject root in scene.GetRootGameObjects())
            {
                root.SetActive(state);
            }
        }

        public void MostrarResultados(ScoreSO scoreData, string naveName)
        {
            // 1. Obtener escena Results
            Scene resultsSceneObj = SceneManager.GetSceneByName(resultsScene);

            // 2. Activar todos sus objetos
            SetActiveAllObjects(resultsSceneObj, true);

            // 3. Configurar resultados
            foreach (GameObject root in resultsSceneObj.GetRootGameObjects())
            {
                ResultsController controller = root.GetComponentInChildren<ResultsController>();
                if (controller != null)
                {
                    controller.ConfigurarResultados(scoreData, naveName);
                    break;
                }
            }

            // 4. Pausar el juego
            Time.timeScale = 0f;

            EventManager.InvokeGameEnded();
        }

        private IEnumerator LimpiarEscenasAnteriores()
        {
            // Lista de escenas a descargar
            string[] escenas = { currentGameScene, resultsScene, characterSelectScene };

            foreach (string escena in escenas)
            {
                Scene scene = SceneManager.GetSceneByName(escena);
                if (scene.IsValid() && scene.isLoaded)
                {
                    yield return SceneManager.UnloadSceneAsync(scene);
                }
            }
        }
















        /*
                private IEnumerator CargarEscenasDeJuego(PlayerDataSO naveSeleccionada)
                {
                    // Descargar Game y Results si están cargadas
                    if (SceneManager.GetSceneByName(currentGameScene).isLoaded)
                        yield return SceneManager.UnloadSceneAsync(currentGameScene);

                    if (SceneManager.GetSceneByName(resultsScene).isLoaded)
                        yield return SceneManager.UnloadSceneAsync(resultsScene);

                    // Descargar CharacterSelect si está cargada
                    if (SceneManager.GetSceneByName(characterSelectScene).isLoaded)
                        yield return SceneManager.UnloadSceneAsync(characterSelectScene);

                    // Cargar Game y Results
                    AsyncOperation gameLoad = SceneManager.LoadSceneAsync(currentGameScene, LoadSceneMode.Additive);
                    AsyncOperation resultsLoad = SceneManager.LoadSceneAsync(resultsScene, LoadSceneMode.Additive);

                    yield return new WaitUntil(() => gameLoad.isDone && resultsLoad.isDone);

                    // Activar escena Game como activa
                    SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentGameScene));

                    // Iniciar juego
                    GameManager gm = FindObjectOfType<GameManager>();
                    if (gm != null)
                    {
                        gm.IniciarJuego(naveSeleccionada);
                    }
                    else
                    {
                        Debug.LogError("GameManager no encontrado en la escena Game");
                    }

                    EventManager.InvokeGameStarted(naveSeleccionada);
                }

                private IEnumerator LimpiarEscenasAnteriores()
                {
                    string[] escenas = { currentGameScene, resultsScene, characterSelectScene };
                    foreach (string escena in escenas)
                    {
                        if (SceneManager.GetSceneByName(escena).isLoaded)
                            yield return SceneManager.UnloadSceneAsync(escena);
                    }
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
            EventManager.InvokeGameEnded();  // Invocar evento de fin del juego
        }
         */
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
            EventManager.InvokeSceneChanged();
        }

        public void LoadSceneAdditiveAsync(string sceneName, Action callback = null)
        {
            if (SceneManager.GetSceneByName(sceneName).isLoaded) return; // ✅ Prevenir recarga
            StartCoroutine(LoadSceneCoroutine(sceneName, callback));
        }
        private IEnumerator LoadSceneCoroutine(string sceneName, Action callback)
        {
            if (SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                callback?.Invoke();
                yield break;
            }

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            yield return new WaitUntil(() => asyncOperation.isDone);

            callback?.Invoke();
        }
    }

}