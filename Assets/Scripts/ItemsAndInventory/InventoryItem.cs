using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// 仓库物品类，包含物品数据和堆叠数量
/// </summary>

[Serializable]
public class InventoryItem
{
    public ItemData Data;
    public int StackSize;

    public InventoryItem(ItemData _itemData)
    {
        Data = _itemData;
        StackSize=1;
    }
    public void AddStack() => StackSize++;
    public void RemoveStack() => StackSize--;
}
