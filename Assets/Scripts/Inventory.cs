using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;


    [SerializeField] private List<InventoryItem> inventoryItems;

    private Dictionary<ItemData, InventoryItem> inventoryDictionary;
    [SerializeField] private Transform inventoryParentSlot;

    private UI_ItemSlot[] ItemSlots;
    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        inventoryItems = new List<InventoryItem>();
        inventoryDictionary=new Dictionary<ItemData, InventoryItem>();
        ItemSlots = inventoryParentSlot.GetComponentsInChildren<UI_ItemSlot>();
    }
    public void UpdateUI_Slot()
    {
        for(int i=0;i<inventoryItems.Count;i++)
        {
            ItemSlots[i].UpdateSlot(inventoryItems[i]);
        }
    }
    public void AddItem(ItemData _item)
    {
        if(inventoryDictionary.TryGetValue(_item,out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem _newItem = new InventoryItem(_item);
            inventoryItems.Add(_newItem);
            inventoryDictionary.Add(_item, _newItem);
        }
        UpdateUI_Slot();
    }
    public void RemoveItem(ItemData _item)
    {
        if(inventoryDictionary.TryGetValue(_item,out InventoryItem value))
        {
            if(value.StackSize<=1)
            {
                inventoryItems.Remove(value);
                inventoryDictionary.Remove(_item);
            }
            else
            {
                value.RemoveStack();
            }
        }
        UpdateUI_Slot();
    }


}
