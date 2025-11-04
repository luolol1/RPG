using UnityEngine;

public enum ItemType
{
    Equipment,
    Material
}
[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public ItemType itemType;
    public string ItemName;
    public Sprite Icon;

}
