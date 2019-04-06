using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Maze mazePrefab;

	private Maze mazeInstance;

    public Player playerPrefab;

    private Player playerInstance;

    private float scale = 0.1f; //will change later so that get from Maze script

    private void Start () {
        StartCoroutine(BeginGame());
        //BeginGame();
	}
	
	private void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			RestartGame();
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
    }

    //private void BeginGame () {
    //	mazeInstance = Instantiate(mazePrefab) as Maze;
    //	StartCoroutine(mazeInstance.Generate());
    //}

    private void RestartGame () {
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