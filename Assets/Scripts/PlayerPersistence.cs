using UnityEngine;

public static class PlayerPersistence
{
    public static void SaveData(PlayerSettings setting)
    {
        PlayerPrefs.SetInt("numberofplayer", setting.NumberOfPlayers);
        PlayerPrefs.SetInt("timelimit", setting.TimeLimit);
        PlayerPrefs.SetString("color", setting.Color);
    }

    public static PlayerSettings LoadData()
    {
        int numberOfPlayers = PlayerPrefs.GetInt("numberofplayer", 1);
        int timeLimit = PlayerPrefs.GetInt("timelimit", 60);
        string color = PlayerPrefs.GetString("color", "default");

        PlayerSettings playerSettings = new PlayerSettings()
        {
            NumberOfPlayers = numberOfPlayers,
            TimeLimit = timeLimit,
            Color = color
        };

        return playerSettings;
    }
}
