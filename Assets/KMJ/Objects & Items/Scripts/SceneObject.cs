using UnityEngine;

public class SceneObject : MonoBehaviour
{
    [SerializeField] private int sceneObjectID = -1;
    public int ID { get { return sceneObjectID; } }
    [SerializeField] private string sceneObjectName = "Object Name";
    public string ObjectName { get { return sceneObjectName; } }
    [SerializeField] private bool isInteractible = false;
    [SerializeField] private int ObjectIDAfterInteraction = -1;
    [SerializeField] private int[] ItemIDsObtainedAfterInteraction;
    [SerializeField] private int ItemIDRequiredForInteraction = -1;

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
    }
}
