using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Armor,
    Amulet,
    Flask
}

[CreateAssetMenu(fileName = "New Item Data",menuName ="Data/Equipment")]
public class EquipmentItem : ItemData
{
    public EquipmentType equipmentType;
}
