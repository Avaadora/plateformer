using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    [SerializeField] private List<Item> ContentInventory = new List<Item>(); // Inventaire qui contient les ITEM
    [SerializeField] private GameObject PrefabSlotInventory;
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

    void Start()
    {

    }

    public void UseItem()
    {
        Debug.Log("Item sorti de l'inventaire pour le cuisiner");
        //ContentInventory.Remove(currentItem) -> pour enlever l'élément de la liste et donc de l'inventaire
    }

    public void AddItemToInventory(Item ItemToPickUp)
    {
        GameObject slot = Instantiate(PrefabSlotInventory, ParentInventory.transform);
        if (!ContentInventory.Contains(ItemToPickUp))
        {
            ContentInventory.Add(ItemToPickUp);
            Debug.Log(ItemToPickUp);
            Instantiate(ItemToPickUp, slot.transform); //Prends toujours le cookie, oskur

            // PrefabSlotInventory.GetComponent<Image>().sprite = ItemToPickUp.ItemSprite; // Affichage du sprite dans l'UI
        }
    }
}