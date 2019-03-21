using UnityEngine;

public static class PlayerPersistence
{
    public static void SaveData(PlayerSettings setting)
    {
        PlayerPrefs.SetInt("mazeWidth", setting.MazeWidth);
        PlayerPrefs.SetInt("mazeHeight", setting.MazeHeight);
        PlayerPrefs.SetInt("numberofplayer", setting.NumberOfPlayers);
        PlayerPrefs.SetInt("timelimit", setting.TimeLimit);
        PlayerPrefs.SetString("color", setting.Color);
    }

    public static PlayerSettings LoadData()
    {
        int mazeWidth = PlayerPrefs.GetInt("mazeWidth", 10);
        int mazeHeight = PlayerPrefs.GetInt("mazeHeight", 10);
        int numberOfPlayers = PlayerPrefs.GetInt("numberofplayer", 1);
        int timeLimit = PlayerPrefs.GetInt("timelimit", 60);
        string color = PlayerPrefs.GetString("color", "default");

        PlayerSettings playerSettings = new PlayerSettings()
        {
            MazeWidth = mazeWidth,
            MazeHeight = mazeHeight,
            NumberOfPlayers = numberOfPlayers,
            TimeLimit = timeLimit,
            Color = color
        };

        return playerSettings;
    }
}
