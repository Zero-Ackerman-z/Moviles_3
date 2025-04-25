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

    public SceneMusic[] musicPerScene;
    public AudioSource musicSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        PlayMusicForScene(SceneManager.GetActiveScene().name);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene == SceneManager.GetActiveScene())
        {
            PlayMusicForScene(scene.name);
        }
    }

    private void PlayMusicForScene(string sceneName)
    {
        foreach (SceneMusic sm in musicPerScene)
        {
            if (sm.sceneName == sceneName)
            {
                if (musicSource.clip != sm.musicClip)
                {
                    musicSource.clip = sm.musicClip;
                    musicSource.Play();
                }
                return;
            }
        }

        Debug.LogWarning($"No se encontró música asignada para la escena: {sceneName}");
    }

    
}
