using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] private Item Item;

    private InputController InputAction;

    void Awake()
    {
        InputAction = new InputController();
        InputAction.Player.Enable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.CompareTag("Player"));
        if (collision.CompareTag("Player") && Item.isDiggable)
        {
            // Vérifier si l'objet appartient à la recette de cracher du feu
            if (InputAction.Player.Dig.IsPressed())
            {
                RecipeManager.Instance.CheckForFireRecipe(Item);
                gameObject.SetActive(false);
            }
            // gameObject.SetActive(false);
        }
        if (collision.CompareTag("Player"))
        {
            // Vérifier si l'objet appartient à la recette de planer
            if (Item.Tag.Equals("Glide"))
            {
                RecipeManager.Instance.CheckForGlideRecipe(Item);
                gameObject.SetActive(false);
            }

            // Vérifier si l'objet appartient à la recette de creuser
            if (Item.Tag.Equals("Dig"))
            {
                RecipeManager.Instance.CheckForDigRecipe(Item);
                gameObject.SetActive(false);
            }

            // Vérifier si l'objet appartient à la recette de cracher du feu
            if (Item.Tag.Equals("WallJump"))
            {
                RecipeManager.Instance.CheckForWallJumpRecipe(Item);
                gameObject.SetActive(false);
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



    private void Respawn()
    {
        gameObject.SetActive(true);
    }
}
