using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] private Item Item;

    [SerializeField] private Animator animator;

    private InputController InputAction;
    private AudioManager audioManager;

    UnityEvent ButtonEPressed;

    void Awake()
    {
        InputAction = new InputController();
        InputAction.Player.Enable();

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        if (ButtonEPressed == null)
            ButtonEPressed = new UnityEvent();

        ButtonEPressed.AddListener(EPressed);
    }

    void Update()
    {
        if (Input.anyKeyDown && ButtonEPressed != null)
        {
            ButtonEPressed.Invoke();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Vérifier si l'objet appartient à la recette de planer
            if (Item.Tag.Equals("Glide"))
            {
                RecipeManager.Instance.CheckForGlideRecipe(Item);
                gameObject.SetActive(false);
                AudioManager._Instance.PlaySFX(audioManager.PickUp1);
            }

            // Vérifier si l'objet appartient à la recette de creuser
            if (Item.Tag.Equals("Dig"))
            {
                RecipeManager.Instance.CheckForDigRecipe(Item);
                gameObject.SetActive(false);
                AudioManager._Instance.PlaySFX(audioManager.PickUp3);
            }

            // Vérifier si l'objet appartient à la recette de cracher du feu
            if (Item.Tag.Equals("WallJump"))
            {
                RecipeManager.Instance.CheckForWallJumpRecipe(Item);
                gameObject.SetActive(false);
                AudioManager._Instance.PlaySFX(audioManager.PickUp2);
            }

            if (RecipeManager.Instance.getIsInOrder())
            {
                animator.SetTrigger("IsPickedUp");
            }
        }
        Invoke(nameof(Respawn), 5f);




        // if (!(RecipeManager.Instance.getCanGlide() || RecipeManager.Instance.getCanDig() || RecipeManager.Instance.getCanFire()))
        // {
        //     Invoke(nameof(Respawn), 5f);
        // }
        // else
        // {
        //     gameObject.SetActive(false);
        // }
    }

    private void OnCollisionStay2D(Collision2D other)
    {

        if (InputAction.Player.Dig.IsPressed())
        {
            if (other.collider.CompareTag("Player") && Item.isDiggable)
            {
                // Vérifier si l'objet appartient à la recette de cracher du feu
                RecipeManager.Instance.CheckForFireRecipe(Item);
                gameObject.SetActive(false);
                // gameObject.SetActive(false);
            }
        }
    }

    private void Respawn()
    {
        gameObject.SetActive(true);
    }

    private void EPressed()
    {
        Debug.Log("E is pressed");
    }
}
