using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemSlot : MonoBehaviour
{
    public Image ItemImage;
    public TextMeshProUGUI ItemText;

    public InventoryItem Item;
    public void UpdateSlot(InventoryItem _newitem)
    {
        Item = _newitem;
        ItemImage.color = Color.white;
        if(Item!=null)
        {
            ItemImage.sprite = Item.itemData.Icon;
            if(Item.StackSize>1)
            {
                ItemText.text = Item.StackSize.ToString();
            }
            else
            {
                ItemText.text = "";
            }
        }
    }
}
