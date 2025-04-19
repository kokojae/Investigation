using UnityEngine;

public class SceneObject : MonoBehaviour
{
    [SerializeField] private int sceneObjectID = -1;
    public int ID { get { return sceneObjectID; } }
    [SerializeField] private string sceneObjectName = "Object Name";
    public string ObjectName { get { return sceneObjectName; } }
    [SerializeField] private bool isInteractible = false;
    [SerializeField] private GameObject ObjectAfterInteraction = null;
    [SerializeField] private InventoryItemData[] ItemsObtainedAfterInteraction;
    [SerializeField] private InventoryItemData ItemRequiredForInteraction = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        Debug.Log(name + " Clicked in OnMouseDown");

        // 상호작용 불가능 오브젝트라면 종료
        if (!isInteractible)
        {
            Debug.Log("상호작용 불가능 오브젝트");
            return;
        }

        // 상호작용에 필요한 오브젝트가 있다면
        if (ItemRequiredForInteraction != null)
        {
            Debug.Log("상호작용에 필요한 오브젝트: " + ItemRequiredForInteraction.ItemName);
            // 지금 플레이어가 선택한 오브젝트가 상호작용에 필요한 오브젝트가 아니라면 종료
            if (Inventory.instance.selectedItemID != ItemRequiredForInteraction.ID)
            {
                Debug.Log("지금 플레이어가 선택한 오브젝트가 상호작용에 필요한 오브젝트가 아님");
                return;
            }
        }

        // 상호작용 후 오브젝트가 있다면 생성
        if (ObjectAfterInteraction != null)
        {
            Debug.Log("상호작용 후 오브젝트 생성");
            Instantiate(ObjectAfterInteraction, transform.position, transform.rotation);
        }
        // 상호작용이 끝난 오브젝트 삭제
        Destroy(this);

        // 상호 작용 후 지급 할 아이템을 지급
        for (int i = 0; i < ItemsObtainedAfterInteraction.Length; i++)
        {
            Debug.Log("아이템 지급");
            Inventory.instance.AddItem(ItemsObtainedAfterInteraction[i]);
        }
    }
}
