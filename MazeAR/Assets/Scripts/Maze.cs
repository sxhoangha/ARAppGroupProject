using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public float generationStepDelay;
    public MazeCell cellPrefab;
    private MazeCell[,] cells;
    public IntVector2 size;
    public IntVector2 RandomCoordinates
    {
        get
        {
            return new IntVector2(Random.Range(0, size.x), Random.Range(0, size.z));
        }
    }

    public bool ContainsCoordinates (IntVector2 coordinate)
    {
        // Return true as long as the vector size falls within [0, 20] (20 here is the default size.x && size.z)
        return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Generate()
    {
        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        cells = new MazeCell[size.x, size.z];
        // Create list of maze cell
        List<MazeCell> activeCells = new List<MazeCell>();
        DoFirstGenerationStep(activeCells);
        IntVector2 coordinates = RandomCoordinates;
        while(activeCells.Count > 0)
        {
            yield return delay;
            DoNextGenerationStep(activeCells);
        }
    }

    private MazeCell CreateCell(IntVector2 coordinates)
    {
        MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
        cells[coordinates.x, coordinates.z] = newCell;
        newCell.coordinates = coordinates;
        // newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
        newCell.transform.parent = transform;
        newCell.transform.localPosition =
            new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f + 0.5f);
        return newCell;
    }

    public MazeCell GetCell(IntVector2 coordinates)
    {
        // Get a MazeCell, for future validation in Generate()
        return cells[coordinates.x, coordinates.z];
    }

    private void DoFirstGenerationStep(List<MazeCell> activeCells)
    {
        // Inititate the first cell into List
        activeCells.Add(CreateCell(RandomCoordinates));
    }

    private void DoNextGenerationStep(List<MazeCell> activeCells)
    {
        int currentIndex = activeCells.Count - 1;
        // Get current cell (the last cell drawn)
        MazeCell currentCell = activeCells[currentIndex];
        // Randomly generate a new direction
        MazeDirection direction = MazeDirections.RandomValue;
        // Draw a new cell based on new direction
        IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();
        // If drawable, add new cell to list, it will eventually become the currentCell by next loop
        if (ContainsCoordinates(coordinates) && GetCell(coordinates) == null)
        {
            activeCells.Add(CreateCell(coordinates));
        }
        else
        {
            // if new cell bump to an existing cell, remove it
            activeCells.RemoveAt(currentIndex);
        }
    }
}
