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

    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && InputAction.Player.Dig.triggered)
        {
            if (Item.isDiggable && Item.Tag.Equals("Diggable"))
            {
                // Vérifier si l'objet appartient à la recette de cracher du feu
                RecipeManager.Instance.CheckForFireRecipe(Item);
                AudioManager._Instance.PlaySFX(audioManager.PickUp1);
            }
            if (RecipeManager.Instance.getIsInOrder())
            {
                animator.SetTrigger("IsPickedUp");
                gameObject.SetActive(false);
            }
            else
            {
                Invoke(nameof(Respawn), 5f);
            }

        }
        // Invoke(nameof(Respawn), 5f);

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
                gameObject.SetActive(false);
            }
            else
            {
                Invoke(nameof(Respawn), 5f);
            }
        }
        // Invoke(nameof(Respawn), 5f);

        if (RecipeManager.Instance.getIsInOrder())
        {
            gameObject.SetActive(false);

        }
        else
        {
            Invoke(nameof(Respawn), 5f);
        }
    }




    private void Respawn()
    {
        gameObject.SetActive(true);
    }
}
