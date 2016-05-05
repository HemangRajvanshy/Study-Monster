using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class InGameItems : MonoBehaviour {

    public List<Item> Items = new List<Item>();

    public Item GetByName(string name)
    {
        foreach(Item item in Items)
        {
            if(item.Name == name)
            {
                return item;
            }
        }
        return null;
    }
}
