using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    public ItemData itemData;
    public int StackSize;

    public InventoryItem(ItemData _itemData)
    {
        itemData = _itemData;
        StackSize=1;
    }
    public void AddStack() => StackSize++;
    public void RemoveStack() => StackSize--;
}
