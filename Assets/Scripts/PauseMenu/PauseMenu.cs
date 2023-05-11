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
    public GameObject creditsMenuUI;
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
        creditsMenuUI.SetActive(false);

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

    public void CreditsToggle()
    {
       
        if (creditsMenuUI.activeSelf == true)
        {
            creditsMenuUI.SetActive(false);
           
        }
        else
        {
            creditsMenuUI.SetActive(true);
          
        } 
    }
    public void ControlsToggle()
    {
        if (controlsMenuUI.activeSelf == true)
        {
            controlsMenuUI.SetActive(false);
           
        }
        else
        {
            controlsMenuUI.SetActive(true);
          
        } 
    }
    
    public void ToggleAudioSettings()
    {
     
        if (audioSettingsPanel.activeSelf == true)
        {
            audioSettingsPanel.SetActive(false);
         
        }
        else
        {
            audioSettingsPanel.SetActive(true);
     
        }
    }


    public void SetOverallVolume(float volume)
    {
        Debug.Log("You touched me wtf?");
        mainMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }
    
}