using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int ItemCount;
    [SerializeField] List<Item> ContentInventory = new List<Item>();

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

    public void UseItem()
    {
        Debug.Log("Item sorti de l'inventaire pour le cuisiner");
        //ContentInventory.Remove(currentItem) -> pour enlever l'élément de la liste et donc de l'inventaire
    }

    public void AddItem(int count)
    {
        ItemCount += count;
    }

    public void RemoveItem(int count)
    {
        ItemCount -= count;
    }
}