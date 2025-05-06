using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class SettingsMenu : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Slider volumeSlider;

    private Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;

        // TMP_Dropdown �ʱ�ȭ
        resolutionDropdown.ClearOptions();
        var options = new List<string>();
        int currentIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string text = $"{resolutions[i].width} x {resolutions[i].height}";
            options.Add(text);
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentIndex;
        resolutionDropdown.RefreshShownValue();

        // Toggle / Slider �ʱ�ȭ
        fullscreenToggle.isOn = Screen.fullScreen;
        volumeSlider.value = AudioListener.volume;

        // �̺�Ʈ ���ε�
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetResolution(int idx)
    {
        var r = resolutions[idx];
        Screen.SetResolution(r.width, r.height, Screen.fullScreen);
    }

    public void SetFullscreen(bool isFull)
    {
        Screen.fullScreen = isFull;
    }

    public void SetVolume(float vol)
    {
        AudioListener.volume = vol;
    }

    public void OffCanvas()
    {
        GetComponentInParent<Transform>().gameObject.SetActive(false);
    }
}
