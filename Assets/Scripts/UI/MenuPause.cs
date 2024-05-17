using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    private InputController InputActions;

    private bool isPaused;

    private AudioManager audioManager;

    void Awake()
    {
        InputActions = new InputController();
        InputActions.UI.Enable();

        InputActions.UI.Pause.performed += ctx => TogglePause();

        CloseWindow();

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void Pause()
    {
        if (InputActions.UI.Pause.IsPressed() && !isPaused)
        {
            gameObject.SetActive(true);
            isPaused = true;
        }
    }

    public void Home()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void Restart()
    {
        RecipeManager.Instance.RestartGameRecipe();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        AudioManager._Instance.PlaySFX(audioManager.UiButton);
    }

    public void CloseWindow()
    {
        gameObject.SetActive(false);
    }

    public void Resume()
    {
        gameObject.SetActive(false);
        isPaused = false;
        AudioManager._Instance.PlaySFX(audioManager.UiButton);
    }

    public void QuitGame()
    {
        Application.Quit();
        AudioManager._Instance.PlaySFX(audioManager.UiButton);
    }

    void TogglePause()
    {
        if (!isPaused)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }
}
