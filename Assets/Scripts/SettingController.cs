using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingController : MonoBehaviour
{
    public Text lblWidth;
    public Text lblHeight;
    public Slider sldWidth;
    public Slider sldHeight;
    public Dropdown cboNumPlayers;
    public Dropdown cboTimeLimit;
    public Dropdown cboColor;

    void Start()
    {
        int mazeWidth = PlayerPrefs.GetInt("MazeWidth");
        int mazeHeight = PlayerPrefs.GetInt("MazeHeight");
        int numPlayers = PlayerPrefs.GetInt("NumPlayers");
        int timeLimit = PlayerPrefs.GetInt("TimeLimit");
        string mazeColor = PlayerPrefs.GetString("MazeColor");

        // initialization when no stored values
        if (mazeWidth == 0)
        {
            mazeWidth = 10;
            PlayerPrefs.SetInt("MazeWidth", mazeWidth);
            Debug.Log("MazeWidth Initialized: " + mazeWidth);
        }
        if (mazeHeight == 0)
        {
            mazeHeight = 10;
            PlayerPrefs.SetInt("MazeHeight", mazeHeight);
            Debug.Log("MazeHeight Initialized: " + mazeWidth);
        }

        // Commented Out because it's not decided which kind of values are used for these
        /*
        if (numPlayers == 0)
        {
            numPlayers = 1;
            PlayerPrefs.SetInt("NumPlayers", numPlayers);
            Debug.Log("Numplayers Initialized: " + mazeWidth);
        }
        if (timeLimit == 0)
        {
            timeLimit = 60;
            PlayerPrefs.SetInt("TimeLimit", timeLimit);
            Debug.Log("TimeLimit Initialized: " + mazeWidth);
        }
        */

        // displaying
        lblWidth.text = mazeWidth.ToString();
        lblHeight.text = mazeHeight.ToString();
        sldWidth.value = mazeWidth;
        sldHeight.value = mazeHeight;
        cboNumPlayers.value = numPlayers;


        // not implementated yet
        // cboTimeLimit.value = timeLimit;
    }


    void Update()
    {

    }

    public void Reset()
    {
        // initializing
        PlayerPrefs.SetInt("MazeWidth", 10);
        PlayerPrefs.SetInt("MazeHeight", 10);
        PlayerPrefs.SetInt("NumPlayers", 1);
        PlayerPrefs.SetInt("TimeLimit", 60);
        PlayerPrefs.SetString("Color", "default");

        // get values from PlayerPrefs
        int mazeWidth = PlayerPrefs.GetInt("MazeWidth");
        int mazeHeight = PlayerPrefs.GetInt("MazeHeight");
        int numPlayers = PlayerPrefs.GetInt("NumPlayers");
        int timeLimit = PlayerPrefs.GetInt("TimeLimit");
        int mazeColor = PlayerPrefs.GetInt("MazeColor");

        // displaying
        lblWidth.text = mazeWidth.ToString();
        lblHeight.text = mazeHeight.ToString();
        sldWidth.value = mazeWidth;
        sldHeight.value = mazeHeight;
        cboNumPlayers.value = numPlayers;

        Debug.Log("Setting values reinitialized!");
    }
    public void Save()
    {
        // initializing
        PlayerPrefs.SetInt("MazeWidth", (int) sldWidth.value);
        PlayerPrefs.SetInt("MazeHeight", (int) sldHeight.value);
        PlayerPrefs.SetInt("NumPlayers", cboNumPlayers.value);
        PlayerPrefs.SetInt("TimeLimit", cboTimeLimit.value);
        PlayerPrefs.SetInt("MazeColor", cboTimeLimit.value);

        // get values from PlayerPrefs
        int mazeWidth = PlayerPrefs.GetInt("MazeWidth");
        int mazeHeight = PlayerPrefs.GetInt("MazeHeight");
        int numPlayers = PlayerPrefs.GetInt("NumPlayers");
        int timeLimit = PlayerPrefs.GetInt("TimeLimit");
        int mazeColor = PlayerPrefs.GetInt("MazeColor");

        // displaying
        lblWidth.text = mazeWidth.ToString();
        lblHeight.text = mazeHeight.ToString();
        sldWidth.value = mazeWidth;
        sldHeight.value = mazeHeight;
        cboNumPlayers.value = numPlayers;
        cboTimeLimit.value = timeLimit;
        // cboColor.value = mazeColor;

        Debug.Log("Setting values Saved!");
    }


}
