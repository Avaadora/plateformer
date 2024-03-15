using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    [SerializeField] private List<Item> ContentInventory = new List<Item>();
    [SerializeField] private List<Image> SlotsInventory = new List<Image>();
    [SerializeField] private Image PrefabSlotInventory;
    [SerializeField] private GameObject ParentInventory;
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
        SlotsInventory.Add(PrefabSlotInventory);

        // Récupération dynamique des slots d'items, pour ne pas avoir de limites dans l'inventaire
        foreach (var slot in SlotsInventory)
        {
            if (slot.sprite == null && ContentInventory.Contains(ItemToPickUp))
            {
                foreach (var item in ContentInventory)
                {
                    Instantiate(PrefabSlotInventory, ParentInventory.transform);
                    slot.sprite = item.ItemSprite;
                }
            }
        }
    }
}