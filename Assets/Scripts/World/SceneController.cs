using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public UnityEvent OnSceneIntro;

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
            // SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    void OnDestroy()
    {
        if (_instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            _instance = null;
        }
    }
    [Header("------------SceneController------------")]
    [SerializeField] private Animator SceneTransition;
    private void FixedUpdate()
    {
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CheckIntro();
    }

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

    private void CheckIntro()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1 && OnSceneIntro != null)
        {
            OnSceneIntro.Invoke();
        }
    }

    public bool getIsSceneIntro()
    {
        return SceneManager.GetActiveScene().buildIndex == 1;
    }

}
