using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    public Inventory inventory;

    public InventoryItemData[] items;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Addtem(0);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Addtem(1);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Addtem(2);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Addtem(3);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Addtem(4);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Removetem(0);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Removetem(1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Removetem(2);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Removetem(3);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            Removetem(4);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            inventory.ClearInvectory();
        }
    }

    void Addtem(int n)
    {
        inventory.AddItem(items[n]);
    }

    void Removetem(int n)
    {
        inventory.RemoveItem(items[n]);
    }
}
