using UnityEditor;
using UnityEngine;

public class RoomChange : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] private BackgroundSlide[] rooms;
    [SerializeField] private ScreenTransition levelLoader;
    public int currentRoomIndex;

    [SerializeField] private float roomSpace;
    private Vector3[] roomPositions;
    private Vector3[] cameraPositions;
    private int nextRoomIndex;

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
        levelLoader.gameObject.SetActive(false);
        nextRoomIndex = 0;
        MoveCamera();
    }

    public void RoomChangeStart(int roomIndex)
    {
        nextRoomIndex = roomIndex;
        levelLoader.gameObject.SetActive(true);
    }

    public void MoveCamera()
    {
        if (nextRoomIndex >= rooms.Length || nextRoomIndex < 0)
        {
            Debug.Log("룸 인덱스 초과 오류");
            return;
        }
        rooms[currentRoomIndex].DeactiveRoom();
        currentRoomIndex = nextRoomIndex;
        mainCamera.transform.position = cameraPositions[currentRoomIndex];
        rooms[currentRoomIndex].SetCurrentRoom();
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
        EditorGUILayout.PrefixLabel("커스텀 인터페이스");
        EditorGUILayout.EndHorizontal();
        nextRoomIndex = EditorGUILayout.IntField("룸 인덱스", nextRoomIndex);

        if (GUILayout.Button("이동"))
            roomChange.RoomChangeStart(nextRoomIndex);

        if (GUI.changed)
            EditorUtility.SetDirty(roomChange);
    }
}
