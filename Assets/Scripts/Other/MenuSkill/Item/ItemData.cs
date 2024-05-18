using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newItemsData", menuName ="Item/Items Data")]

public class ItemData : ScriptableObject
{
    public Item[] items;

    public int GetItemsLength()
    {
        return items.Length;
    }

    public Item GetItem(int id)
    {
        return items[id];
    }
}
