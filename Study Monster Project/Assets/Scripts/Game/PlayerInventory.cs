using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class PlayerInventory : MonoBehaviour {

    private Dictionary<Item, int> Items;
}

[Serializable]
public class Item
{
    public string Name;
    public Sprite Image;
}