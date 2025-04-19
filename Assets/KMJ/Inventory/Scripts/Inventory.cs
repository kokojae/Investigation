using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton    
    public static Inventory instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("인벤토리 인스턴스가 1개가 아님");
            return;
        }

        instance = this;
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;

    public delegate void OnSelectedChanged();
    public OnSelectedChanged OnSelectedChangedCallback;

    public int space = 9;

    public List<InventoryItemData> items = new List<InventoryItemData>();

    public int selectedItemID = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void CallItemChanged()
    {
        if (OnItemChangedCallback != null)
        {
            OnItemChangedCallback.Invoke();
        }
    }

    void CallSelectedChanged()
    {
        if (OnSelectedChangedCallback != null)
        {
            OnSelectedChangedCallback.Invoke();
        }
    }

    public bool AddItem(InventoryItemData item)
    {
        if (items.Count >= space)
        {
            Debug.Log("인벤토리 가득 참");
            return false;
        }

        items.Add(item);

        CallItemChanged();

        CallSelectedChanged();

        return true;
    }

    public void RemoveItem(InventoryItemData item)
    {
        if (selectedItemID == item.GetInstanceID())
        {
            selectedItemID = 0;
        }

        items.Remove(item);

        CallItemChanged();

        CallSelectedChanged();
    }

    public void SelectItem(InventoryItemData item)
    {
        selectedItemID = item.GetInstanceID();

        CallSelectedChanged();
    }

    public void ClearInvectory()
    {
        items.Clear();
        
        selectedItemID = 0;

        CallItemChanged();

        CallSelectedChanged();
    }
}
