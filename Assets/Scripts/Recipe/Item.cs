using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory Item")]
public class Item : ScriptableObject
{
    [SerializeField] public int Index;
    [SerializeField] public string ItemName;
    [SerializeField] private string ItemDescription;
    [SerializeField] public string Tag;
    [SerializeField] public Sprite ItemSprite;

}
