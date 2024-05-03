using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    private InputController InputActions;

    private bool isPaused;

    void Awake()
    {
        InputActions = new InputController();
        InputActions.UI.Enable();

        InputActions.UI.Pause.performed += ctx => TogglePause();

        CloseWindow();
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void CloseWindow()
    {
        gameObject.SetActive(false);
    }

    public void Resume()
    {
        gameObject.SetActive(false);
        isPaused = false;
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
