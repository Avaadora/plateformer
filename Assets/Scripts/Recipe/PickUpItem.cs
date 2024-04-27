using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] private Item Item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Vérifier si l'objet appartient à la recette de planer
            if (Item.Tag.Equals("Glide"))
            {
                RecipeManager.Instance.CheckForGlideRecipe(Item);
            }

            // Vérifier si l'objet appartient à la recette de creuser
            if (Item.Tag.Equals("Dig"))
            {
                RecipeManager.Instance.CheckForDigRecipe(Item);
            }
        }
        Destroy(gameObject);
    }
}
