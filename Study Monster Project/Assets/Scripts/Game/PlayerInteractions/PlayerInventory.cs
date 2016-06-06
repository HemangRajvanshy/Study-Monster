using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class PlayerInventory : MonoBehaviour {

    private Dictionary<Item, int> Items;
    private List<int> TextbookPages = new List<int>();

    public List<int> GetAvailableText()
    {
        return TextbookPages;
    }

    public void AddPageToText(int num)
    {
        if(!TextbookPages.Contains(num))
            TextbookPages.Add(num);
    }
}

[Serializable]
public class Item
{
    public string Name;
    public Sprite Image;
}
