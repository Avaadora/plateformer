using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    private InputController MyInputActions;

    private bool isPaused;
    void Awake()
    {
        MyInputActions = new InputController();
        MyInputActions.UI.Enable();

        CloseWindow();
    }
    

    public void Pause()
    {
        Debug.Log(MyInputActions.UI.Pause.IsPressed());

        if (MyInputActions.UI.Pause.IsPressed() && !isPaused)
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
}
