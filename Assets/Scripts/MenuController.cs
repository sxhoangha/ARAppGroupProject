using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("PlayGame");
    }

    public void MazeCreation()
    {
        SceneManager.LoadScene("MazeCreation");
    }

    public void Home()
    {
        SceneManager.LoadScene("Home");
    }

    public void Restart()
    {
        SceneManager.LoadScene("MazeCreation"); // for now
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void HighScore()
    {
        SceneManager.LoadScene("HighScore");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Clear()
    {
        DropdownController.instance.ResetDropDown();
    }

}
