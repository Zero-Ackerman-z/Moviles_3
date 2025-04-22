using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [System.Serializable]
    public class SceneMusic
    {
        public string sceneName;
        public AudioClip musicClip;
    }

    [Header("Lista de música por escena")]
    public List<SceneMusic> sceneMusicList;

    private AudioSource audioSource;
    private string currentScene;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
        ReproducirMusicaParaEscena(currentScene);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != currentScene)
        {
            currentScene = scene.name;
            ReproducirMusicaParaEscena(currentScene);
        }
    }

    private void ReproducirMusicaParaEscena(string sceneName)
    {
        SceneMusic sceneMusic = sceneMusicList.Find(m => m.sceneName == sceneName);

        if (sceneMusic != null && sceneMusic.musicClip != null)
        {
            audioSource.clip = sceneMusic.musicClip;
            audioSource.Play();
        }
        else
        {
            audioSource.Stop(); 
        }
    }

    public void CambiarVolumen(float volumen)
    {
        audioSource.volume = volumen;
    }
}
