using UnityEditor;
using UnityEngine;

public class RoomChange : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] private GameObject[] rooms;
    public int currentRoomIndex;

    [SerializeField] private float roomSpace;
    private Vector3[] roomPositions;
    private Vector3[] cameraPositions;

    private void Start()
    {
        roomPositions = new Vector3[rooms.Length];
        cameraPositions = new Vector3[rooms.Length];

        for (int i = 0; i < rooms.Length; i++)
        {
            roomPositions[i] = new Vector3(0, 0 - roomSpace * i, 0);
            cameraPositions[i] = new Vector3(mainCamera.transform.position.x, 0 - roomSpace * i, mainCamera.transform.position.z);
            rooms[i].transform.position = roomPositions[i];
        }

        MoveCamera(0);
    }

    public void MoveCamera(int nextRoomIndex)
    {
        if (nextRoomIndex >= rooms.Length || nextRoomIndex < 0)
        {
            Debug.Log("���� �� �ε����� ������ ���");
            return;
        }

        currentRoomIndex = nextRoomIndex;
        mainCamera.transform.position = cameraPositions[currentRoomIndex];
    }
}

[CustomEditor(typeof(RoomChange))]
public class RoomChangeEditor : Editor
{
    int nextRoomIndex;
    RoomChange roomChange;

    void OnEnable()
    {
        roomChange = (RoomChange)target;
        nextRoomIndex = roomChange.currentRoomIndex;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Ŀ���� ������");
        EditorGUILayout.EndHorizontal();
        nextRoomIndex = EditorGUILayout.IntField("�̵��� �� �ε���", nextRoomIndex);

        if (GUILayout.Button("�̵�"))
            roomChange.MoveCamera(nextRoomIndex);

        if (GUI.changed)
            EditorUtility.SetDirty(roomChange);
    }
}
