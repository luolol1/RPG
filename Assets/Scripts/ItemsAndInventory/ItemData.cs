using UnityEngine;


public enum ItemType
{
    Equipment,
    Material
}

/// <summary>
/// 物品数据类，包含物品类型、名称和图标等基本信息
/// </summary>
[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public ItemType itemType;
    public string ItemName;
    public Sprite Icon;

}
