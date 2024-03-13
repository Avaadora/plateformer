using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    [SerializeField] List<Item> ContentInventory = new List<Item>();
    [SerializeField] Dictionary<GameObject, SpriteRenderer> InventoryUI = new Dictionary<GameObject, SpriteRenderer>();
    [SerializeField] private SpriteRenderer ItemSpriteUI;

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
        ItemSpriteUI.GetComponent<SpriteRenderer>();
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
        LinkedToUI(ItemToPickUp);
    }

    public void LinkedToUI(Item ItemStored)
    {
        for (int i = 0; i < InventoryUI.Count; i++)
        {
            InventoryUI.Add(gameObject.transform.GetChild(i).GetChild(0).gameObject, ItemSpriteUI); // InventoryUI : récupère l'enfant de l'enfant du Item_Panel avec son sprite

            if (ContentInventory.Contains(ItemStored))
            {
                ItemSpriteUI = ItemStored.ItemSprite;
            }
        }
    }
}