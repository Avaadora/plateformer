using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private InventoryDisplay Display;
    private InventoryData data;

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        int slotCount = Display.Initialize();

        //data = new InventoryData();
    }

    public Item AddItem(Item _item)
    {
        return _item;
    }

    public Item PickItem(int IDSlot)
    {
        return new Item();
    }
}

// Affichage de l'inventaire
public class InventoryDisplay : MonoBehaviour
{
    private Slot[] slots;
    public int Initialize()
    {
        slots = GetComponentsInChildren<Slot>();
        return slots.Length;
    }

    public void UpdateDisplay(Item[] dataArray)
    {

    }
}

public class InventoryData
{
    public InventoryData(int slotCount)
    {
        items = new Item[slotCount];
    }

    public Item[] items { private set; get;}
}

