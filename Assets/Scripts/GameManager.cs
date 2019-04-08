using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Maze mazePrefab;
    public Player playerPrefab;
    public GameObject panelGameOver;    // Minseok 2019/04/07
    public Text labelElpsdTime;    // Minseok 2019/04/07

    private Maze mazeInstance;
    private Player playerInstance;
    private float scale = 0.1f; //will change later so that get from Maze script
    private float startTime;
    private bool isRunning = false;

    private void Start()
    {
        StartCoroutine(BeginGame());
        //BeginGame();
    }

    private void Update()
    {
        // display elapsed time
        if (isRunning == true)
        {
            labelElpsdTime.text = "Elapsed Time: " + (int)(Time.time - startTime) + "s";
        }
        // restart game by putting space key
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }

        // check goalCount
        int goalCount = PlayerPrefs.GetInt("GoalCount");
        if (goalCount >= 1)
        {
            isRunning = false;
            panelGameOver.SetActive(true);
            PlayerPrefs.DeleteKey("GoalCount");
            Debug.Log("Game Over!");
        }
    }


    //Add player
    private IEnumerator BeginGame()
    {
        //Camera.main.clearFlags = CameraClearFlags.Skybox;
        //Camera.main.rect = new Rect(0f, 0f, 1f, 1f);
        mazeInstance = Instantiate(mazePrefab) as Maze;
        yield return StartCoroutine(mazeInstance.Generate());
        playerInstance = Instantiate(playerPrefab) as Player;
        playerInstance.transform.localScale = scale * playerInstance.transform.localScale;
        playerInstance.SetLocation(mazeInstance.GetCell(mazeInstance.RandomCoordinates));
        //Camera.main.clearFlags = CameraClearFlags.Depth;
        //Camera.main.rect = new Rect(0f, 0f, 0.4f, 0.4f);
        isRunning = true;
        startTime = Time.time;
    }

    //private void BeginGame () {
    //	mazeInstance = Instantiate(mazePrefab) as Maze;
    //	StartCoroutine(mazeInstance.Generate());
    //}

    private void RestartGame()
    {
        StopAllCoroutines();
        Destroy(mazeInstance.gameObject);
        //BeginGame();
        if (playerInstance != null)
        {
            Destroy(playerInstance.gameObject);
        }
        StartCoroutine(BeginGame());
    }
}