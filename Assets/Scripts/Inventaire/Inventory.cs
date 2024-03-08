using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<Item> ContentInventory = new List<Item>();
    private Sprite InventoryUI;
    public static Inventory instance;

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
        InventoryUI = GetComponent<Sprite>();
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

    public void LinkedToUI(Item ItemToPickUp)
    {
        foreach (var item in ContentInventory)
        {
            if (true)
            {

            }

        }


    }



    // public void RemoveItem(int count)
    // {
    //     // ItemCount -= count;
    // }
}