using UnityEngine;

public class SceneObject : MonoBehaviour
{
    [SerializeField] private int sceneObjectID = -1;
    public int ID { get { return sceneObjectID; } }
    [SerializeField] private string sceneObjectName = "Object Name";
    public string ObjectName { get { return sceneObjectName; } }

    [SerializeField] private bool isRemoveAfterInteractible = false;
    [SerializeField] private GameObject objectAfterInteraction = null;
    [SerializeField] private InventoryItemData[] itemsObtainedAfterInteraction;
    [SerializeField] private InventoryItemData itemRequiredForInteraction = null;
    [SerializeField] private Collider requiredForZoom = null;
    public bool IsCanZoom
    {
        get
        {
            if (requiredForZoom == null) return false;
            else return true;
        }
    }


    private bool isZoom = false;
    protected bool IsZoom { get { return isZoom; } }

    // 중복 실행 방지 플래그
    private bool hasResolved = false;

    /// <summary>퍼즐이 해결되었음을 부모에 알리고 후처리</summary>
    public virtual void ResolvePuzzle()
    {
        if (hasResolved) return;
        hasResolved = true;

        GrantRewards();
        SpawnObjectAfterInteraction();
        CleanupObject();
    }

    protected void DefaulfClickInterection()
    {
        HandleItemRequirement();
        ResolvePuzzle();
    }

    /// <summary>아이템 요구 사항 검사</summary>
    protected bool HandleItemRequirement()
    {
        if (itemRequiredForInteraction == null)
            return true;

        Debug.Log("상호작용에 필요한 오브젝트: " + itemRequiredForInteraction.ItemName);
        if (Inventory.instance.selectedItemID != itemRequiredForInteraction.ID)
        {
            Debug.Log("지금 플레이어가 선택한 오브젝트가 상호작용에 필요한 오브젝트가 아님");
            return false;
        }
        return true;
    }

    /// <summary>아이템 지급 처리</summary>
    protected void GrantRewards()
    {
        if (itemsObtainedAfterInteraction.Length == 0)
        {
            Debug.Log("지급할 아이템 없음");
            return;
        }

        if (Inventory.instance == null)
        {
            Debug.Log("인벤토리 존재 하지 않음");
            return;
        }

        foreach (var item in itemsObtainedAfterInteraction)
        {
            Debug.Log("아이템 지급: " + item.ItemName);
            Inventory.instance.AddItem(item);
        }
    }

    /// <summary>상호작용 후 오브젝트 생성</summary>
    protected void SpawnObjectAfterInteraction()
    {
        if (objectAfterInteraction == null)
            return;

        Debug.Log("상호작용 후 오브젝트 생성");
        Instantiate(objectAfterInteraction, transform.position, transform.rotation);
    }

    /// <summary>오브젝트 정리(삭제) 처리</summary>
    protected void CleanupObject()
    {
        // 생성된 오브젝트를 위해 게임오브젝트 삭제
        if (objectAfterInteraction != null)
        {
            Destroy(gameObject);
            return;
        }

        if (isRemoveAfterInteractible)
        {
            Destroy(gameObject);
        }
    }

    private void SafetyEnabled(Collider target, bool value)
    {
        if (target != null)
        {
            target.enabled = value;
        }
    }

    public virtual void ObjectZoom()
    {
        Debug.Log("오브젝트 확대됨");
        SafetyEnabled(requiredForZoom, false);
        isZoom = true;
    }

    public virtual void ObjectUnZoom()
    {
        Debug.Log("오브젝트 확대 취소됨");
        SafetyEnabled(requiredForZoom, true);
        isZoom = false;
    }
}
