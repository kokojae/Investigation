using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class InventoryItemData : ScriptableObject
{
    [SerializeField] private int itemID = -1;
    public int ID { get { return itemID; } }

    [SerializeField] private string itemName = "Item";
    public string ItemName { get { return itemName; } }

    [SerializeField] private Sprite itemSprite;
    public Sprite ItemSprite { get { return itemSprite; } }

    [SerializeField] private GameObject item3DPrefab;
    public GameObject Item3DPrefab { get { return item3DPrefab; } }

}
