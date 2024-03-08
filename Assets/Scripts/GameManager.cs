using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Player
    /*-------------VARIABLES PLAYER-------------*/
    [SerializeField] private float Speed; //Vitesse du player
    [SerializeField] private float Smoothing; //Valeur de smoothing accélération au départ et ralentissement lors de l'arrêt du joueur
    private bool isJumping;
    private bool isGrounded;
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
