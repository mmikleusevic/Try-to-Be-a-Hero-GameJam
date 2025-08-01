using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private TextMeshProUGUI volumeMusicText;
    [SerializeField] private TextMeshProUGUI volumeSFXText;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    private bool isPaused;
    private MouseLook mouseLook;
    
    private void Start()
    {
        mouseLook = FindAnyObjectByType<MouseLook>();
        volumeSFXText.text = $"{Mathf.Round(sfxVolumeSlider.value * 100)}%";
        volumeMusicText.text = $"{Mathf.Round(musicVolumeSlider.value * 100)}%";
        musicVolumeSlider.value = AudioManager.Instance.GetMusicVolume();
        sfxVolumeSlider.value = AudioManager.Instance.GetSFXVolume();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void ContinueGame()
    {
        ResumeGame();
    }

    public void BackToMainMenu()
    {
        LevelManager.Instance.LoadScene(Scenes.MainMenu);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ChangeMusicVolume(float volume)
    {
        AudioManager.Instance.ChangeVolume(volume);
    }

    public void ChangeSFXVolume(float volume)
    {
        AudioManager.Instance.ChangeSFXVolume(volume);
    }

    private void PauseGame()
    {
        if (mouseLook) mouseLook.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        pausePanel.SetActive(true);

        Time.timeScale = 0f;
        isPaused = true;
    }

    private void ResumeGame()
    {
        if (mouseLook) mouseLook.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        
        pausePanel.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;
    }
}