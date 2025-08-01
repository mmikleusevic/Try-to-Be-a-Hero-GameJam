using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Panels")] [SerializeField]
    private GameObject mainMenuPanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private Slider volumeSoundSlider;
    [SerializeField] private Slider volumeSFXSlider;
    [SerializeField] private TMP_Text volumeSoundText;
    [SerializeField] private TMP_Text volumeSFXText;
    [SerializeField] private GameObject storyPanelGameObject;
    
    private StoryPanel storyPanel;

    private void Awake()
    {
        storyPanel = GetComponent<StoryPanel>();
    }

    private void Start()
    {
        InitializePanels();
    }

    private void OnEnable()
    {
        storyPanel.DisableMainMenu += DisableMainMenu;
    }

    private void OnDisable()
    {
        storyPanel.DisableMainMenu -= DisableMainMenu;
    }

    private void InitializePanels()
    {
        OnVolumeChangedSound();
        OnVolumeChangedSFX();
        mainMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void PlayGame()
    {
        storyPanelGameObject.gameObject.SetActive(true);
        StartCoroutine(storyPanel.TypeText());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OptionsButton()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void MainMenuButton()
    {
        mainMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void OnVolumeChangedSound()
    {
        volumeSoundText.text = $"{Mathf.Round(volumeSoundSlider.value * 100)}%";
        AudioManager.Instance.ChangeVolume(volumeSoundSlider.value);
    }

    public void OnVolumeChangedSFX()
    {
        volumeSFXText.text = $"{Mathf.Round(volumeSFXSlider.value * 100)}%";
        AudioManager.Instance.ChangeSFXVolume(volumeSFXSlider.value);
    }

    private void DisableMainMenu()
    {
        mainMenuPanel.SetActive(false);
    }
}