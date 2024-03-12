using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    [SerializeField] List<Item> ContentInventory = new List<Item>();
   // [SerializeField] private GameObject InventoryUI;
    
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
        //InventoryUI = GetComponent<GameObject>();
    }

    public void UseItem()
    {
        Debug.Log("Item sorti de l'inventaire pour le cuisiner");
        //ContentInventory.Remove(currentItem) -> pour enlever l'élément de la liste et donc de l'inventaire
    }

    public void AddItemToInventory(Item ItemToPickUp)
    {
        //Debug.Log(ItemToPickUp);
        ContentInventory.Add(ItemToPickUp);
    }

//     public void LinkedToUI(Item ItemStored)
//     {
//         if (ContentInventory.Contains(ItemStored) && InventoryUI.gameObject.CompareTag("Slot_Item_Sprite"))
//         {
//             //Remplacer le sprite de l'image vide par celui de l'item
//             //InventoryUI = ItemStored.ItemImage;
//         }
//     }
}