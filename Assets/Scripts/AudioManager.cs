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

    [System.Serializable]
    public class SFXClip
    {
        public string name;
        public AudioClip clip;
    }

    [Header("Música por Escena")]
    public SceneMusic[] musicPerScene;
    public AudioSource musicSource;

    [Header("Efectos de Sonido")]
    public SFXClip[] sfxClips;
    public AudioSource sfxSource;

    private Dictionary<string, AudioClip> sfxDictionary;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); 
        CrearDiccionarioSFX();
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
    private void CrearDiccionarioSFX()
    {
        sfxDictionary = new Dictionary<string, AudioClip>();
        foreach (var sfx in sfxClips)
        {
            if (!sfxDictionary.ContainsKey(sfx.name))
            {
                sfxDictionary.Add(sfx.name, sfx.clip);
            }
        }
    }

    public void PlaySFX(string name)
    {
        if (sfxDictionary.TryGetValue(name, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"No se encontró el SFX con nombre: {name}");
        }
    }

}
