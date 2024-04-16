using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalAudioManager : MonoBehaviour
{
    [System.Serializable]
    public class SceneAudioClip
    {
        public string sceneName;
        public AudioClip backgroundMusic;
    }

    public SceneAudioClip[] sceneAudios;
    private AudioSource audioSource;

    // Singleton pattern to ensure only one instance is active
    public static GlobalAudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scene changes
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find the SceneAudioClip that matches the newly loaded scene
        SceneAudioClip sac = System.Array.Find(sceneAudios, item => item.sceneName == scene.name);
        if (sac != null && sac.backgroundMusic != null)
        {
            audioSource.clip = sac.backgroundMusic;
            audioSource.Play();
        }
    }

    // Additional methods to control audio (e.g., play SFX) can be added here
}
