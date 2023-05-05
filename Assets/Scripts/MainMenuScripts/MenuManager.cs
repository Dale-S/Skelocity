using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;


public class MenuManager : MonoBehaviour
{
    public AudioMixer mainMixer;
    public Button startButton;
    public Button soundsButton;
    public Button exitButton;
    public GameObject audioSettingsPanel;

    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        soundsButton.onClick.AddListener(ToggleAudioSettings);
        exitButton.onClick.AddListener(ExitGame);
        audioSettingsPanel.SetActive(false);
    }

    void StartGame()
    {
        SceneManager.LoadScene("Hub");
    }

    void ToggleAudioSettings()
    {
        audioSettingsPanel.SetActive(!audioSettingsPanel.activeSelf);
    }
    public void SetOverallVolume(float volume)
    {
        mainMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }


    void ExitGame()
    {
        Application.Quit();
    }
}

