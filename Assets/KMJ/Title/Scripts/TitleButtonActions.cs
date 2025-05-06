using UnityEngine;

public class TitleButtonActions : MonoBehaviour
{
    [SerializeField] private GameObject SettingCanvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameStart()
    {

    }

    public void OnSetting()
    {
        SettingCanvas.SetActive(true);
    }
}
