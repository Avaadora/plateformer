using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    private static SceneController _instance;
    public static SceneController Instance
    {
        get
        {
            return _instance;
        }
    }
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Debug.LogWarning("Second instance of SceneController created. Automatic self - destruct triggered.");
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
    [Header("------------SceneController------------")]
    [SerializeField] private Animator SceneTransition;
    public void LoadNextScene()
    {
        StartCoroutine(LevelTransition());
    }

    IEnumerator LevelTransition()
    {
        SceneTransition.SetTrigger("End");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneTransition.SetTrigger("Start");
    }

}
