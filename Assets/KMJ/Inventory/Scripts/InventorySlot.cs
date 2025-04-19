using JetBrains.Annotations;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;

    public Image selectRect;

    InventoryItemData item;

    public int ItemID
    {
        get { return itemID; }
        private set { itemID = value; }
    }
    int itemID;

    public void AddItem(InventoryItemData newItem)
    {
        item = newItem;

        icon.sprite = item.ItemSprite;
        icon.enabled = true;

        itemID = newItem.ID;
    }

    public void ClearSlot()
    {
        icon.enabled = true;
        icon.sprite = null;

        item = null;

        if (selectRect.enabled)
        {
            selectRect.enabled = false;
        }
    }

    public void Select()
    {
        if (item != null)
        {
            Inventory.instance.SelectItem(item);
        }
    }
}
