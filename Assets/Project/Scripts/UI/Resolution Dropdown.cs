using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ResolutionDropdown : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;

    private UnityEngine.Resolution[] resolutions; 

    void Start()
    {
        // Get available resolutions
        resolutions = Screen.resolutions;

        // Clear existing options
        resolutionDropdown.ClearOptions();

        // Create a list of resolution options
        List<string> options = new List<string>();

        // Add each resolution option to the list
        foreach (UnityEngine.Resolution res in resolutions) 
        {
            string option = res.width + " x " + res.height;
            options.Add(option);
        }

        // Add the options to the dropdown
        resolutionDropdown.AddOptions(options);

        // Set the current resolution as the default selected option
        resolutionDropdown.value = GetCurrentResolutionIndex();
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        UnityEngine.Resolution resolution = resolutions[resolutionIndex]; 
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    private int GetCurrentResolutionIndex()
    {
        UnityEngine.Resolution currentResolution = Screen.currentResolution; 

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == currentResolution.width && resolutions[i].height == currentResolution.height)
            {
                return i;
            }
        }

        return 0; // Return default resolution index if current resolution is not found
    }
}
