using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour, IPointerClickHandler
{
    public GameObject pauseMenuUI;
    public GameObject controlsMenuUI;
    public AudioMixer mainMixer;
    public Button hubButton;
    public Button soundsButton;
    public Button exitButton;
    public GameObject audioSettingsPanel;
    
    private bool isPaused = false;

    private void Start()
    {
        hubButton.onClick.AddListener(TeleportToScene);
        exitButton.onClick.AddListener(ExitGame);
        audioSettingsPanel.SetActive(false);
        pauseMenuUI.SetActive(false);
        controlsMenuUI.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerPress == soundsButton.gameObject)
        {
            ToggleAudioSettings();
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void TeleportToScene()
    {
        Resume();
        SceneManager.LoadScene("Hub");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void ToggleAudioSettings()
    {
        Debug.Log("ToggleAudioSettings called");
        if (audioSettingsPanel.activeSelf == true)
        {
            audioSettingsPanel.SetActive(false);
            Debug.Log("Panel deactivated");
        }
        else
        {
            audioSettingsPanel.SetActive(true);
            Debug.Log("Panel activated");
        }
    }


    public void SetOverallVolume(float volume)
    {
        mainMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }
    
}