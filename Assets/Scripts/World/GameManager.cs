using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("------------GameManager------------")]
    private static GameManager _Instance;
    public UnityEvent OnSceneTuto = new UnityEvent();
    public UnityEvent OnSceneLevel = new UnityEvent();
    public UnityEvent OnSceneIntro = new UnityEvent();

    public static GameManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                var obj = new GameObject().AddComponent<GameManager>();
                obj.name = "GameManager Object";
                _Instance = obj.GetComponent<GameManager>();
            }
            return _Instance;
        }
    }

    private void Awake()
    {
        if (_Instance != null)
        {
            Debug.LogWarning("Second instance of GameManager created. Automatic self - destruct triggered.");
            Destroy(gameObject);
        }

    }

    private void OnDestroy()
    {
        if (_Instance == this)
        {
            _Instance = null;
        }
    }

    private void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void DialogueScene()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2 && OnSceneTuto != null)
        {
            OnSceneTuto.Invoke();
        }
        else
        {
            if (SceneManager.GetActiveScene().buildIndex == 3 && OnSceneLevel != null)
            {
                OnSceneLevel.Invoke();
            }
        }
    }

    public bool getIsTutoScene()
    {
        return SceneManager.GetActiveScene().buildIndex == 2;
    }

    public bool getIsLevelScene()
    {
        return SceneManager.GetActiveScene().buildIndex == 3;
    }

    [Header("------------Player------------")]
    /*-------------VARIABLES PLAYER-------------*/
    private float Speed = 6f; // Vitesse du player
    private float Smoothing = 0.3f; // Valeur de smoothing accélération au départ et ralentissement lors de l'arrêt du joueur
    private float JumpForce = 4f;
    private float GlideSpeed = 1.25f;
    private float CoyoteTime = 0.2f;

    private bool isJumping, isGrounded;
    /*-------------GETTERS & SETTERS PLAYER-------------*/
    public bool getIsJumping()
    {
        return isJumping;
    }
    public void setIsJumping(bool isJumping)
    {
        this.isJumping = isJumping;
    }

    public bool getIsGrounded()
    {
        return isGrounded;
    }

    public void setIsGrounded(bool isGrounded)
    {
        this.isGrounded = isGrounded;
    }

    public float getSpeed()
    {
        return Speed;
    }

    public void setSpeed(float SpeedPlayer)
    {
        Speed = SpeedPlayer;
    }

    public float getSmoothing()
    {
        return Smoothing;
    }

    public void setSmoothing(float SmoothingWalk)
    {
        Smoothing = SmoothingWalk;
    }

    public float getJumpForce()
    {
        return JumpForce;
    }

    public void setJumpForce(float JumpForce)
    {
        this.JumpForce = JumpForce;
    }

    public float getCoyoteTime()
    {
        return CoyoteTime;
    }

    public void setCoyoteTime(float CoyoteTime)
    {
        this.CoyoteTime = CoyoteTime;
    }

    public float getGlideSpeed()
    {
        return GlideSpeed;
    }

    public void setGlideSpeed(float GlideSpeed)
    {
        this.GlideSpeed = GlideSpeed;
    }
}
