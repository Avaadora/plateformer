using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private AudioManager audioManager;
    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void PlayGame()
    {
        AudioManager._Instance.PlaySFX(audioManager.UiButton);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PauseGame()
    {
        AudioManager._Instance.PlaySFX(audioManager.UiButton);
    }

    public void QuitGame()
    {
        AudioManager._Instance.PlaySFX(audioManager.UiButton);
        Application.Quit();
    }
}