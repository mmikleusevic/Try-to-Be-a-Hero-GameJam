using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioClip mainMenuMusic;
    public AudioClip gameMusic;

    private AudioSource audioSource;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); 
        audioSource = GetComponent<AudioSource>();
        SceneManager.sceneLoaded += OnSceneLoaded; 
    }

    private void Start()
    {
        PlayMusic(mainMenuMusic);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            PlayMusic(mainMenuMusic);
        }
        else if (scene.name == "Game") 
        {
            PlayMusic(gameMusic);
        }
    }

    private void PlayMusic(AudioClip clip)
    {
        if (clip == null || audioSource == null) return;

        

        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }
}