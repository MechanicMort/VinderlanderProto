using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/ItemSO", order = 3)]

public class Item : ScriptableObject
{
    public int worth;
    public string itemName;
    public GameObject inWorldItem;
    public Sprite itemSprite;
}
