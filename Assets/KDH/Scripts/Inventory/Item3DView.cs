using UnityEngine;

public class Item3DView : MonoBehaviour
{
    [SerializeField] private Transform viewer;
    [SerializeField] Transform itemsParent;
    private Inventory inventory;
    private GameObject currentViewingItem;
    private int currentViewingItemID;

    private void Start()
    {
        inventory = Inventory.instance;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            for (int i = 0; i < inventory.items.Count; i++)
            {
                if (inventory.items[i].ID == inventory.selectedItemID)
                {
                    if (currentViewingItem == null)
                        Instantiate3DViewItem(inventory.items[i].Item3DPrefab);
                    
                    if (currentViewingItemID != inventory.selectedItemID)
                    {
                        Destroy(currentViewingItem.gameObject);
                        Instantiate3DViewItem(inventory.items[i].Item3DPrefab);
                    }
                    else
                        return;
                    

                    break;
                }
            }
        }
    }

    private void Instantiate3DViewItem(GameObject item)
    {
        currentViewingItem =
            Instantiate(item, Vector3.zero, Quaternion.identity, viewer);

        currentViewingItemID = inventory.selectedItemID;
    }
}
