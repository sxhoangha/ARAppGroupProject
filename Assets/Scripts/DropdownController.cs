using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DropdownController : MonoBehaviour
{
    public static DropdownController instance;

    public Text lblWidth;
    public Text lblHeight;
    public Slider sldWidth;
    public Slider sldHeight;
    public Dropdown ddlNumOfPlayers;
    public Dropdown ddlTimeLimit;
    public Dropdown ddlColor;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        // add listner to when a user changes options or values in the sliders or the Dropdowns
        sldWidth.onValueChanged.AddListener(delegate { DropDownValueChanged(); });
        sldHeight.onValueChanged.AddListener(delegate { DropDownValueChanged(); });
        ddlNumOfPlayers.onValueChanged.AddListener(delegate { DropDownValueChanged(); });
        ddlTimeLimit.onValueChanged.AddListener(delegate { DropDownValueChanged(); });
        ddlColor.onValueChanged.AddListener(delegate { DropDownValueChanged(); });

    }

    void Start()
    {
        // load PlayerSettings including PlayerPrefs
        PlayerSettings settings = PlayerPersistence.LoadData();

        // load the values from the previous setting
        lblWidth.text = settings.MazeWidth.ToString();
        lblHeight.text = settings.MazeHeight.ToString();
        sldWidth.value = settings.MazeWidth;
        sldHeight.value = settings.MazeHeight;

        var listAvailableStrings = ddlNumOfPlayers.options.Select(option => option.text).ToList();
        ddlNumOfPlayers.value = listAvailableStrings.IndexOf(settings.NumberOfPlayers.ToString());
        var listAvailableStrings2 = ddlTimeLimit.options.Select(option => option.text).ToList();
        ddlTimeLimit.value = listAvailableStrings2.IndexOf(settings.TimeLimit.ToString() + "s");
        var listAvailableStrings3 = ddlColor.options.Select(option => option.text).ToList();
        ddlColor.value = listAvailableStrings3.IndexOf(settings.Color);
    }

    public void ResetDropDown()
    {
        sldWidth.value = 10;
        sldHeight.value = 10;
        ddlNumOfPlayers.value = 0;
        ddlTimeLimit.value = 0;
        ddlColor.value = 0;
    }

    private void DropDownValueChanged()
    {
        int mazeWidth = Convert.ToInt32(sldWidth.value);
        int mazeHeight = Convert.ToInt32(sldHeight.value);
        int numberofplayers = Convert.ToInt32(ddlNumOfPlayers.options[ddlNumOfPlayers.value].text);
        int timelimit = Convert.ToInt32(ddlTimeLimit.options[ddlTimeLimit.value].text.Replace("s", ""));
        string color = ddlColor.options[ddlColor.value].text;

        // assigning new values to the PlayerSettings
        PlayerSettings settings = new PlayerSettings()
        {
            MazeWidth = mazeWidth,
            MazeHeight = mazeHeight,
            NumberOfPlayers = numberofplayers,
            TimeLimit = timelimit,
            Color = color
        };
        PlayerPersistence.SaveData(settings);

        // display changed value
        lblWidth.text = settings.MazeWidth.ToString();
        lblHeight.text = settings.MazeHeight.ToString();
    }
}
