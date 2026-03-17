using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EquipmentSlot : UI_ItemSlot
{
    public EquipmentType equipmentType;
    private void OnValidate()
    {
        gameObject.name = "Equipment Slot-" + equipmentType.ToString();
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        //脱掉装备
        Inventory.instance.UnInstallEquipment(Item.Data as ItemData_Equipment);

        //将装备放回至仓库
        Inventory.instance.AddItem(Item.Data as ItemData_Equipment);
        CleanUp();
    }
}
