using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;

    Inventory inventory;

    InventorySlot[] slots;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory = Inventory.instance;
        inventory.OnItemChangedCallback += UpdateUI;

        inventory.OnSelectedChangedCallback += UpdateSelected;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    void UpdateSelected()
    {
        for (int i = 0; i < inventory.items.Count; i++)
        {
            if (slots[i].ItemID == inventory.selectedItemID)
            {
                slots[i].selectRect.enabled = true;
            }
            else
            {
                slots[i].selectRect.enabled = false;
            }
        }
    }
}
