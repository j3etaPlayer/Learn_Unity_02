using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetRe : MonoBehaviour
{
    FullScreenMode screenMode;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullScreenBtn;

    [Header("해상도를 저장할 콜랙션")]
    List<Resolution> resolutions = new(0);
    int currentResolutionNum;

    private void Start()
    {
        Loadcompoments();
    }

    private void Loadcompoments()
    {
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            resolutions.Add(Screen.resolutions[i]);
        }

        resolutionDropdown.options.Clear();

        int optionNum = 0;

        foreach(var resolution in resolutions)
        {
            TMP_Dropdown.OptionData option = new();
            option.text = resolution.width + "x" + resolution.height + " " + resolution.refreshRateRatio + "Hz";

            resolutionDropdown.options.Add(option);

            if (resolution.width == Screen.width && resolution.height == Screen.height)
                resolutionDropdown.value = optionNum;

            optionNum++;
        }
        resolutionDropdown.RefreshShownValue();

        fullScreenBtn.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
    }

    public void DropboxOptionChange()
    {
        currentResolutionNum = resolutionDropdown.value;
    }
    public void FullScreenButton()
    {
        screenMode = fullScreenBtn.isOn ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }
    public void ChangeResolution()
    {
        Screen.SetResolution(resolutions[currentResolutionNum].width, 
                             resolutions[currentResolutionNum].height, 
                             screenMode);
    }
}
