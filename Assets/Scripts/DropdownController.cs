using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DropdownController : MonoBehaviour
{
    public static DropdownController instance;
    public Dropdown dropdown1;
    public Dropdown dropdown2;
    public Dropdown dropdown3;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        // add listner to when a user changes option in the Dropdowns
        dropdown1.onValueChanged.AddListener(delegate { DropDownValueChanged(); });
        dropdown2.onValueChanged.AddListener(delegate { DropDownValueChanged(); });
        dropdown3.onValueChanged.AddListener(delegate { DropDownValueChanged(); });
    }

    void Start()
    {
        PlayerSettings settings = PlayerPersistence.LoadData();

        var listAvailableStrings = dropdown1.options.Select(option => option.text).ToList();
        dropdown1.value = listAvailableStrings.IndexOf(settings.NumberOfPlayers.ToString());
        var listAvailableStrings2 = dropdown2.options.Select(option => option.text).ToList();
        dropdown2.value = listAvailableStrings2.IndexOf(settings.TimeLimit.ToString() + "s");
        var listAvailableStrings3 = dropdown3.options.Select(option => option.text).ToList();
        dropdown3.value = listAvailableStrings3.IndexOf(settings.Color);
    }

    public void ResetDropDown()
    {
        dropdown1.value = 0;
        dropdown2.value = 0;
        dropdown3.value = 0;
    }

    private void DropDownValueChanged()
    {
        int numberofplayers = Convert.ToInt32(dropdown1.options[dropdown1.value].text);
        int timelimit = Convert.ToInt32(dropdown2.options[dropdown2.value].text.Replace("s", ""));
        string color = dropdown3.options[dropdown3.value].text;
        PlayerSettings settings = new PlayerSettings()
        {
            NumberOfPlayers = numberofplayers,
            TimeLimit = timelimit,
            Color = color
        };
        PlayerPersistence.SaveData(settings);
    }
}
