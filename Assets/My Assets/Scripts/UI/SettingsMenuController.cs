﻿using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;using UnityEngine.UI;

public class SettingsMenuController : MonoBehaviour
{
	[Space]
	[SerializeField] private Toggle _fullScreen;
	[Space]
	[SerializeField] TMP_Dropdown _resolutionDropdown;
	private Resolution[] _availableResolutions;

	[Space]
	[SerializeField] TMP_Dropdown _qualityDropdown;
	private string[] _qualityLevels;
    // Start is called before the first frame update
    void Start()
    {
		_fullScreen.onValueChanged.AddListener(OnFullScreenChanged);

		_availableResolutions = Screen.resolutions;
		_resolutionDropdown.ClearOptions();
		_resolutionDropdown.onValueChanged.AddListener(OnResolutionChanged);
		_qualityDropdown.onValueChanged.AddListener(OnQualityChanged);
		int currentIndex = 0;
		List<string> options = new List<string>();
		for (int i = 0; i < _availableResolutions.Length; i++)
		{
			if (_availableResolutions[i].width <= 800)
			{
				continue;
			}
			options.Add(_availableResolutions[i].width + "x" + _availableResolutions[i].height);
			if (_availableResolutions[i].width == Screen.currentResolution.width && _availableResolutions[i].height == Screen.currentResolution.height)
			{
				currentIndex = i;
			}
		}
		_resolutionDropdown.AddOptions(options);
		_resolutionDropdown.value = 0;
		_resolutionDropdown.RefreshShownValue();

		_qualityLevels = QualitySettings.names;
		_qualityDropdown.ClearOptions();
		_qualityDropdown.AddOptions(_qualityLevels.ToList());
		int qualityLvl = QualitySettings.GetQualityLevel();
		_qualityDropdown.value = qualityLvl;
		_qualityDropdown.RefreshShownValue();
    }

    void OnDestroy()
    {
	    _fullScreen.onValueChanged.RemoveListener(OnFullScreenChanged);
    }

    private void OnFullScreenChanged(bool value)
    {
	    Screen.fullScreen = value;
    }

    private void OnResolutionChanged(int resolutionIndex)
    {
	    Resolution resolution = _availableResolutions[resolutionIndex];
		Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    void OnQualityChanged(int qualityLvl)
    {
		QualitySettings.SetQualityLevel(qualityLvl, true);
    }
}
