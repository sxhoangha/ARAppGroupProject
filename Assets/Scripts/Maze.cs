using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;

public class Maze : MonoBehaviour
{

    public IntVector2 size;    // it will be set by setting scene automatically

    public MazeCell cellPrefab;
    public MazePassage passagePrefab;
    public MazeWall[] wallPrefabs;
    // public GameObject playerPrefab;   // Minseok 2019/03/28
    public GameObject goalPrefab;   // Minseok 2019/03/28

    // Add door by teru
    public MazeDoor doorPrefab;
    [Range(0f, 1f)]
    public float doorProbability;

    public int numOfGoals;

    // Minseok 2019/03/28
    public int LoadingGague
    {
        get
        {
            if (cells != null)
            {
                int cellCount = 0;
                foreach (var c in cells)
                {
                    if (c != null)
                    {
                        cellCount++;
                    }
                }
                return (cellCount * 100 / (size.x * size.z));
            }
            else
                return 0;
        }
    }
    
    public float generationStepDelay;

    public float scale;     // level of maze scale

    private MazeCell[,] cells;


    void Start()
    {

    }

    public IntVector2 RandomCoordinates
    {
        get
        {
            return new IntVector2(Random.Range(0, size.x), Random.Range(0, size.z));
        }
    }

    public bool ContainsCoordinates(IntVector2 coordinate)
    {
        return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
    }

    public MazeCell GetCell(IntVector2 coordinates)
    {
        return cells[coordinates.x, coordinates.z];
    }

    public IEnumerator Generate()
    {
        // term between each cell generation
        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);

        PlayerSettings settings = PlayerPersistence.LoadData();

        // initialization when no stored values
        if (settings.MazeWidth == 0)
        {
            settings.MazeWidth = 10;
            PlayerPrefs.SetInt("mazeWidth", settings.MazeWidth);
            Debug.Log("MazeWidth Initialized: " + settings.MazeWidth);
        }
        if (settings.MazeHeight == 0)
        {
            settings.MazeHeight = 10;
            PlayerPrefs.SetInt("mazeHeight", settings.MazeHeight);
            Debug.Log("MazeHeight Initialized: " + settings.MazeHeight);
        }
        size.x = settings.MazeWidth;
        size.z = settings.MazeHeight;

        // initialize cell list
        cells = new MazeCell[size.x, size.z];
        List<MazeCell> activeCells = new List<MazeCell>();
        DoFirstGenerationStep(activeCells);
        while (activeCells.Count > 0)
        {
            yield return delay;
            DoNextGenerationStep(activeCells);
        }

        numOfGoals = (size.x * size.z) / 25;
        PlayerPrefs.SetInt("GoalCount", 0);
        // set a goal at edge of the maze
        for (int i = 0; i < numOfGoals; i++)
        {
            CreateGoal();
            yield return delay;
        }

    }

    private void DoFirstGenerationStep(List<MazeCell> activeCells)
    {
        activeCells.Add(CreateCell(RandomCoordinates));
    }

    private void DoNextGenerationStep(List<MazeCell> activeCells)
    {
        int currentIndex = activeCells.Count - 1;
        MazeCell currentCell = activeCells[currentIndex];
        if (currentCell.IsFullyInitialized)
        {
            // If the cell is surrounded by 4 walls
            activeCells.RemoveAt(currentIndex);
            return;
        }
        MazeDirection direction = currentCell.RandomUninitializedDirection;
        IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();
        if (ContainsCoordinates(coordinates))
        {
            MazeCell neighbor = GetCell(coordinates);
            if (neighbor == null)
            {
                neighbor = CreateCell(coordinates);
                CreatePassage(currentCell, neighbor, direction);
                activeCells.Add(neighbor);
            }
            else
            {
                CreateWall(currentCell, neighbor, direction);
                // activeCells.RemoveAt(currentIndex);
                // No longer need to remove the cell, (check has been put above)
            }
        }
        else
        {
            CreateWall(currentCell, null, direction);
            // activeCells.RemoveAt(currentIndex);
            // No longer need to remove the cell, (check has been put above)
        }
    }

    private MazeCell CreateCell(IntVector2 coordinates)
    {
        MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
        cells[coordinates.x, coordinates.z] = newCell;
        newCell.coordinates = coordinates;
        newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
        newCell.transform.localScale = new Vector3(scale, scale, scale);
        newCell.transform.parent = transform;
        newCell.transform.localPosition = new Vector3(scale * (coordinates.x - size.x * 0.5f + 0.5f), 0f, scale * (coordinates.z - size.z * 0.5f + 0.5f));
        return newCell;
    }

    private void CreatePassage(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazePassage prefab = Random.value < doorProbability ? doorPrefab : passagePrefab;
        MazePassage passage = Instantiate(prefab) as MazePassage;
        passage.transform.localScale = new Vector3(scale, scale, scale); //fit scale by teru
        passage.Initialize(cell, otherCell, direction);
        passage = Instantiate(prefab) as MazePassage;
        passage.transform.localScale = new Vector3(scale, scale, scale); //fit sclae of oposite side by teru
        passage.Initialize(otherCell, cell, direction.GetOpposite());
    }

    private void CreateWall(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazeWall wall = Instantiate(wallPrefabs[Random.Range(0, wallPrefabs.Length)]) as MazeWall;
        wall.transform.localScale = new Vector3(scale, scale, scale);
        wall.Initialize(cell, otherCell, direction);
        if (otherCell != null)
        {
            wall.Initialize(otherCell, cell, direction.GetOpposite());
        }
    }

    // Minseok Choi 2019/03/28
    /// <summary>
    /// Gnerate and place a Goal object at the maze edge randomly.
    /// </summary>
    /// <returns>GameObject GoalInstance</returns>
    private GameObject CreateGoal()
    {
        // coordinate to place the goal
        Vector3 coordinates = new Vector3();
        coordinates.x = Random.Range(0, size.x);
        coordinates.z = Random.Range(0, size.z);

        // place the goal at edges
        /*

        MazeDirection direction = (MazeDirection)Random.Range(0, 4);

        switch (direction)
        {
            case MazeDirection.East:
                coordinates.x = size.x - 1;
                coordinates.z = Random.Range(0, size.z);
                break;
            case MazeDirection.North:
                coordinates.x = Random.Range(0, size.x);
                coordinates.z = size.z - 1;
                break;
            case MazeDirection.South:
                coordinates.x = Random.Range(0, size.x);
                break;
            default:    // West
                coordinates.z = Random.Range(0, size.z);
                break;
        }
        */

        // apply scale variable to the coordinates
        coordinates = scale * new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 1, coordinates.z - size.z * 0.5f + 0.5f);

        GameObject instance = Instantiate(goalPrefab);
        instance.name = "Goal";
        instance.transform.localPosition = new Vector3(coordinates.x, coordinates.y, coordinates.z);
        instance.transform.localScale = scale * instance.transform.localScale;
        instance.transform.parent = transform;
        return instance;
    }
}