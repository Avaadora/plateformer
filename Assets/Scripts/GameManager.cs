using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _Instance;
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
            Debug.LogWarning("Second instance of GameManager created.Automatic self - destruct triggered.");
            Destroy(this.gameObject);
        }
    }
    private void OnDestroy()
    {
        if (_Instance == this)
        {
            _Instance = null;
        }
    }
    void OnEnable()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    #region Player
    /*-------------VARIABLES PLAYER-------------*/
    [SerializeField] private float Speed = 10f; // Vitesse du player
    [SerializeField] private float Smoothing = 0.7f; // Valeur de smoothing accélération au départ et ralentissement lors de l'arrêt du joueur
    [SerializeField] private bool isJumping;
    [SerializeField] private bool isGrounded;

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
    #endregion


}
