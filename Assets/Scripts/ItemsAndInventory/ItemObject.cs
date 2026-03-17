using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private ItemData itemdata;

    private void OnValidate()
    {
        GetComponent<SpriteRenderer>().sprite = itemdata.Icon;
        gameObject.name = "item object -" + itemdata.ItemName;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            Inventory.instance.AddItem(itemdata);
            Debug.Log("item:" + itemdata.ItemName);
            Destroy(gameObject);
        }
    }
}
