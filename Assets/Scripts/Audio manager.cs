using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    
    [SerializeField] private AudioClip mainMenuMusic;
    [SerializeField] private AudioClip level1Music;
    [SerializeField] private AudioClip level2Music;
    [SerializeField] private AudioClip level3Music;
    [SerializeField] private AudioClip endGameMusic;
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource SFXAudioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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
        PlayMusic(mainMenuMusic);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            PlayMusic(mainMenuMusic);
        }
        else if (scene.name == "Level1")
        {
            PlayMusic(level1Music);
        }
        else if (scene.name == "Level2")
        {
            PlayMusic(level2Music);
        }
        else if (scene.name == "Level3")
        {
            PlayMusic(level3Music);
        }
        else if(scene.name == "End")
        {
            PlayMusic(endGameMusic);    
        }
    }

    public void ChangeVolume(float volume)
    {
        musicAudioSource.volume = volume;
    }

    public void ChangeSFXVolume(float volume)
    {
        SFXAudioSource.volume = volume;
    }

    public float GetSFXVolume()
    {
        return SFXAudioSource.volume;
    }
    
    public float GetMusicVolume()
    {
        return musicAudioSource.volume;
    }

    private void PlayMusic(AudioClip clip)
    {
        if (clip == null || musicAudioSource == null) return;

        musicAudioSource.volume = GetMusicVolume();
        musicAudioSource.Stop();
        musicAudioSource.clip = clip;
        musicAudioSource.loop = true;
        musicAudioSource.Play();
    }

    public void PlaySFXMusic(AudioClip clip)
    {
        SFXAudioSource.PlayOneShot(clip);
    }

    public void StopSFXMusic()
    {
        SFXAudioSource.Stop();
    }
}