using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory Item")]
public class Item : ScriptableObject
{
    [SerializeField] private string ItemName;
    [SerializeField] private string ItemDescription;
    [SerializeField] private Sprite ItemImage;
    [SerializeField] float Sweetness;
    [SerializeField] float Bitterness;
    
}
