using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Scripts;

public class GameManager : MonoBehaviour
{

    public Maze mazePrefab;
    public Player playerPrefab;
    public GameObject panelGameOver;    // Minseok 2019/04/07
    public Text lblElapsedTime;    // Minseok 2019/04/07
    public Text lblScore;    // Minseok 2019/04/09
    public Text lblRemainedGoals;    // Minseok 2019/04/09
    public Score score;             // Minseok 2019/04/09

    private Maze mazeInstance;
    private Player playerInstance;
    private float scale = 0.1f; //will change later so that get from Maze script
    private float startTime;
    private bool isRunning = false;
    private int numOfGoals = 0;
    private int goalCount = 0;

    private void Start()
    {
        StartCoroutine(BeginGame());
        //BeginGame();
    }

    private void Update()
    {
        int newGoalCount = PlayerPrefs.GetInt("GoalCount");

        if (isRunning)
        {
            // player catch a goal
            if (goalCount != newGoalCount)
            {
                score.AddScore((int)(Time.time - startTime));
                goalCount = newGoalCount;
            }

            // display game status
            lblElapsedTime.text = "Elapsed Time: " + (int)(Time.time - startTime) + "s";
            lblScore.text = "Score: " + score.score;
            lblRemainedGoals.text = "Remained Goals: " + (numOfGoals - goalCount);

            // restart game by putting space key
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RestartGame();
            }

            // check game over
            if (goalCount >= numOfGoals)
            {
                // stop game and display game over panel
                isRunning = false;
                panelGameOver.SetActive(true);
                PlayerPrefs.SetInt("GoalCount", 0);
                Debug.Log("Game Over!");

                // save the score
                score.SaveScore();
                Debug.Log("New Score saved: " + score.score);
            }
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
        numOfGoals = mazeInstance.numOfGoals;

        // initialize Score variable
        score = new Score(0, numOfGoals, 0, mazeInstance.size);

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