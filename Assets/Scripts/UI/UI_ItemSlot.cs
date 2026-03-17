using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ItemSlot : MonoBehaviour, IPointerDownHandler
{
    public Image ItemImage;
    public TextMeshProUGUI ItemText;

    public InventoryItem Item;

    public void UpdateSlot(InventoryItem _newitem)
    {
        Item = _newitem;
        ItemImage.color = Color.white;
        if (Item != null)
        {
            ItemImage.sprite = Item.Data.Icon;
            if (Item.StackSize > 1)
            {
                ItemText.text = Item.StackSize.ToString();
            }
            else
            {
                ItemText.text = "";
            }
        }
    }
    public void CleanUp()
    {
        Item = null;
        ItemImage.color = Color.clear;
        ItemImage.sprite = null;
        ItemText.text = "";
    }
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (Item != null && Item.Data.itemType == ItemType.Equipment)
            Inventory.instance.EquipItem(Item.Data);
    }
}
