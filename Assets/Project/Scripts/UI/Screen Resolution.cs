using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScreenResolution : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown resolutionDropdown;

    [SerializeField]
    private List<Resolution> resolutions = new List<Resolution>();

    private void Start()
    {
        SetupResolutionDropdown();
        StartResolution();
    }

    private void SetupResolutionDropdown()
    {
        List<string> options = new List<string>();
        foreach (var res in resolutions)
        {
            options.Add(res.width + " X " + res.height);
        }
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);
    }

    private void StartResolution()
    {
        int currentResolutionIndex = GetCurrentResolutionIndex();
        if (currentResolutionIndex != -1)
        {
            resolutionDropdown.value = currentResolutionIndex;
        }
        else
        {
            // Add current resolution if not already in the list
            Resolution currentResolution = new()
            {
                width = Screen.width,
                height = Screen.height
            };
            resolutions.Add(currentResolution);
            resolutionDropdown.value = resolutions.Count - 1;
        }
        UpdateResolutionLabel();
    }

    private int GetCurrentResolutionIndex()
    {
        for (int i = 0; i < resolutions.Count; i++)
        {
            if (Screen.width == resolutions[i].width && Screen.height == resolutions[i].height)
            {
                return i;
            }
        }
        return -1;
    }

    public void UpdateResolutionLabel()
    {
        int selectedResolutionIndex = resolutionDropdown.value;
        resolutionDropdown.captionText.text = resolutions[selectedResolutionIndex].width + " X " + resolutions[selectedResolutionIndex].height;
    }

    public void ApplyGraphic()
    {
        int selectedResolutionIndex = resolutionDropdown.value;
        Resolution selectedResolution = resolutions[selectedResolutionIndex];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
    }
}

[System.Serializable]
internal class Resolution
{
    public int width;
    public int height;
}
