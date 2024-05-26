using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigUpItem : MonoBehaviour
{
    [SerializeField] private Item Item;

    [SerializeField] private Animator animator;

    private InputController InputAction;
    private AudioManager audioManager;

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
                gameObject.SetActive(false);
                animator.SetTrigger("IsPickedUp");
            }
            else
            {
                Invoke(nameof(Respawn), 5f);
            }

        }
        // Invoke(nameof(Respawn), 5f);

    }
    private void Respawn()
    {
        gameObject.SetActive(true);
    }
}
