using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    [SerializeField] List<Item> ContentInventory = new List<Item>();
    Image SlotInventory;

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance INVENTORY dans la scène");
            return;
        }
        instance = this;
    }

    private void Start()
    {

    }

    public void UseItem()
    {
        Debug.Log("Item sorti de l'inventaire pour le cuisiner");
        //ContentInventory.Remove(currentItem) -> pour enlever l'élément de la liste et donc de l'inventaire
    }

    public void AddItemToInventory(Item ItemToPickUp)
    {
        ContentInventory.Add(ItemToPickUp);
        foreach (var itemToStore in ContentInventory)
        {
            if (SlotInventory.sprite == null)
            {
                SlotInventory.sprite = itemToStore.ItemSprite;
            }
        }
    }
}