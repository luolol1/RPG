using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;


    [SerializeField] private List<InventoryItem> inventory;//inventory of Equipment
    private Dictionary<ItemData, InventoryItem> inventoryDictionary;

    [SerializeField] private List<InventoryItem> Stash;//inventory of Materials
    private Dictionary<ItemData, InventoryItem> StashDictionary;

    [SerializeField] private List<InventoryItem> Equipment;
    private Dictionary<ItemData_Equipment, InventoryItem> EquipmentDictionary;

    [SerializeField] private Transform inventoryParentSlot;
    [SerializeField] private Transform StashParentSlot;
    [SerializeField] private Transform EquipmentParentSlot;

    private UI_ItemSlot[] inventoryItemSlots;
    private UI_ItemSlot[] StashItemSlots;
    private UI_EquipmentSlot[] EquipmentSlots;
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
        inventory = new List<InventoryItem>();
        inventoryDictionary=new Dictionary<ItemData, InventoryItem>();

        Stash = new List<InventoryItem>();
        StashDictionary = new Dictionary<ItemData, InventoryItem>();

        Equipment = new List<InventoryItem>();
        EquipmentDictionary = new Dictionary<ItemData_Equipment, InventoryItem>();

        inventoryItemSlots = inventoryParentSlot.GetComponentsInChildren<UI_ItemSlot>();
        StashItemSlots = StashParentSlot.GetComponentsInChildren<UI_ItemSlot>();
        EquipmentSlots = EquipmentParentSlot.GetComponentsInChildren<UI_EquipmentSlot>();
    }
    public void UpdateUI_Slot()
    {
        for (int i = 0; i < EquipmentSlots.Length; i++)
        {
            foreach (var item in EquipmentDictionary)
            {
                if (item.Key.equipmentType == EquipmentSlots[i].equipmentType)
                    EquipmentSlots[i].UpdateSlot(item.Value);
            }
        }
        for(int i=0;i<inventoryItemSlots.Length;i++)
        {
            inventoryItemSlots[i].CleanUp();
        }
        for(int i=0;i<StashItemSlots.Length;i++)
        {
            StashItemSlots[i].CleanUp();
        }

        for(int i=0;i<inventory.Count;i++)
        {
            inventoryItemSlots[i].UpdateSlot(inventory[i]);
        }
        for(int i=0;i<Stash.Count;i++)
        {
            StashItemSlots[i].UpdateSlot(Stash[i]);
        }
    }
    public void AddItem(ItemData _item)
    {
        if (_item.itemType == ItemType.Equipment)
            AddToinventory(_item);
        else if(_item.itemType==ItemType.Material)
            AddToStash(_item);

        UpdateUI_Slot();
    }

    public void EquipItem(ItemData _item)
    {
        ItemData_Equipment New_Equipment = _item as ItemData_Equipment;
        ItemData_Equipment oldEquipment=null;
        foreach(var item in EquipmentDictionary)
        {
            if(item.Key.equipmentType==New_Equipment.equipmentType)
            {
                oldEquipment = item.Key;
            }
        }
        if(oldEquipment!=null)
        {
            UnInstallEquipment(oldEquipment);
            AddItem(oldEquipment);
        }
        InventoryItem _newItem = new InventoryItem(New_Equipment);
        Equipment.Add(_newItem);
        EquipmentDictionary.Add(New_Equipment, _newItem);
        New_Equipment.AddModifier();

        RemoveItemFromInventory(New_Equipment);
    }

    public void UnInstallEquipment(ItemData_Equipment oldEquipment)
    {
        if (EquipmentDictionary.TryGetValue(oldEquipment, out InventoryItem value))
        {
            Equipment.Remove(value);
            EquipmentDictionary.Remove(oldEquipment);
            oldEquipment.RemoveModifier();
        }
    }

    private void AddToStash(ItemData _item)
    {
        if (StashDictionary.TryGetValue(_item, out InventoryItem stashvalue))
        {
            stashvalue.AddStack();
        }
        else
        {
            InventoryItem _newItem = new InventoryItem(_item);
            Stash.Add(_newItem);
            StashDictionary.Add(_item, _newItem);
        }
    }

    private void AddToinventory(ItemData _item)
    {
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem _newItem = new InventoryItem(_item);
            inventory.Add(_newItem);
            inventoryDictionary.Add(_item, _newItem);
        }
    }

    public void RemoveItemFromInventory(ItemData _item)
    {
        if(inventoryDictionary.TryGetValue(_item,out InventoryItem value))
        {
            if(value.StackSize<=1)
            {
                inventory.Remove(value);
                inventoryDictionary.Remove(_item);
            }
            else
            {
                value.RemoveStack();
            }
        }
        if(StashDictionary.TryGetValue(_item,out InventoryItem stash_value))
        {
            if(value.StackSize<=1)
            {
                Stash.Remove(stash_value);
                StashDictionary.Remove(_item);
            }
            else
            {
                stash_value.RemoveStack();
            }
        }
        UpdateUI_Slot();
    }


}
