using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory Item")]
public class Item : ScriptableObject
{
    [SerializeField] private int Index;
    [SerializeField] private string ItemName;
    [SerializeField] private string ItemDescription;
    [SerializeField] public Sprite ItemSprite;
    [SerializeField] float Sweetness;
    [SerializeField] float Bitterness;
}
