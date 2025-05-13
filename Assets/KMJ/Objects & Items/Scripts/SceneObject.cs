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
    [SerializeField] private bool isZoomInteract = false;
    [SerializeField] private bool isZoom = false;

    virtual protected bool ZoomInteract() { return false; }

    void OnMouseDown()
    {
        // 상호작용 불가능 오브젝트라면 종료
        if (!isInteractible)
        {
            Debug.Log("상호작용 불가능 오브젝트");
            return;
        }

        // 현재 줌이 되어있고, 줌 이후의 인터렉션이 따로 구현되어야 한다면
        if (isZoom && isZoomInteract)
        {
            // 만약 상호작용 이후 이후의 실행이 필요 없다면 종료
            if (ZoomInteract())
            {
                return;
            }
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
        isZoom = true;
    }

    public void ObjectUnZoom()
    {
        Debug.Log("오브젝트 확대 취소됨");
        SafetyEnabled(activeAfterZoom, false);
        SafetyEnabled(requiredForZoom, true);
        isZoom = false;
    }
}
