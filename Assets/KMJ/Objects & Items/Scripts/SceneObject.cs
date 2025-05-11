using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class SceneObject : MonoBehaviour
{
    [SerializeField] private int sceneObjectID = -1;
    public int ID { get { return sceneObjectID; } }
    [SerializeField] private string sceneObjectName = "Object Name";
    public string ObjectName { get { return sceneObjectName; } }
    [SerializeField] private bool isInteractible = false;
    [SerializeField] private bool isRemoveAfterInteractible = false;
    [SerializeField] private GameObject objectAfterInteraction = null;
    [SerializeField] private InventoryItemData[] itemsObtainedAfterInteraction;
    [SerializeField] private InventoryItemData itemRequiredForInteraction = null;
    [SerializeField] private Collider requiredForZoom = null;
    [SerializeField] private Collider activeAfterZoom = null;

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
        // 상호작용 불가능 오브젝트라면 종료
        if (!isInteractible)
        {
            Debug.Log("상호작용 불가능 오브젝트");
            return;
        }

        // 상호작용에 필요한 오브젝트가 있다면
        if (itemRequiredForInteraction != null)
        {
            Debug.Log("상호작용에 필요한 오브젝트: " + itemRequiredForInteraction.ItemName);
            // 지금 플레이어가 선택한 오브젝트가 상호작용에 필요한 오브젝트가 아니라면 종료
            if (Inventory.instance.selectedItemID != itemRequiredForInteraction.ID)
            {
                Debug.Log("지금 플레이어가 선택한 오브젝트가 상호작용에 필요한 오브젝트가 아님");
                return;
            }
        }

        // 상호 작용 후 지급 할 아이템을 지급
        for (int i = 0; i < itemsObtainedAfterInteraction.Length; i++)
        {
            if (Inventory.instance != null)
            {
                Debug.Log("아이템 지급");
                Inventory.instance.AddItem(itemsObtainedAfterInteraction[i]);
            }
            else
            {
                Debug.Log("인벤토리 존재 하지 않음");
            }
        }

        // 상호작용 후 오브젝트가 있다면 생성
        if (objectAfterInteraction != null)
        {
            Debug.Log("상호작용 후 오브젝트 생성");
            Instantiate(objectAfterInteraction, transform.position, transform.rotation);
            // 오브젝트 겹침 방지를 위해 삭제
            Destroy(this);
            // 중복 삭제를 막기 위한 함수 종료
            return;
        }

        // 상호작용이 끝난 오브젝트 삭제가 필요한 경우 삭제
        if (isRemoveAfterInteractible)
        {
            Destroy(this);
        }
    }

    private void SafetyEnabled(Collider target, bool value)
    {
        if (target != null)
        {
            target.enabled = value;
        }
    }

    public void ObjectZoom()
    {
        Debug.Log("오브젝트 확대됨");
        SafetyEnabled(requiredForZoom, false);
        SafetyEnabled(activeAfterZoom, true);
    }

    public void ObjectUnZoom()
    {
        Debug.Log("오브젝트 확대 취소됨");
        SafetyEnabled(activeAfterZoom, false);
        SafetyEnabled(requiredForZoom, true);
    }
}
