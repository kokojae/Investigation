using UnityEngine;

public class Item3DView : MonoBehaviour
{
    [SerializeField] OrbitCamera orbitCamera;
    [SerializeField] private Transform viewer;
    [SerializeField] Transform itemsParent;
    [SerializeField] RoomChange roomChange;
    private Inventory inventory;
    private GameObject currentViewingItem;
    private int currentViewingItemID;
    private bool is3DViewMode;

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

        if (Input.GetKeyDown(KeyCode.Escape) && is3DViewMode)
        {
            is3DViewMode = false;
            orbitCamera.UnActive();
            roomChange.MoveCamera();
        }
    }

    private void Instantiate3DViewItem(GameObject item)
    {
        is3DViewMode = true;
        
        currentViewingItem =
            Instantiate(item, viewer.position, Quaternion.identity, viewer);
        orbitCamera.target = currentViewingItem.transform;
        orbitCamera.ResetTransform();
        currentViewingItemID = inventory.selectedItemID;
    }
}
