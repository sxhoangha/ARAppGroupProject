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
        IntVector2 coordinates = RandomCoordinates;
        // Keep creating new vector as long as [true]
        while(ContainsCoordinates(coordinates) && GetCell(coordinates) == null)
        {
            // GetCell(coordinates) == null => To make sure to don't draw a cell twice, null means we haven't drawn any cell at that particular coordinate yet
            yield return delay;
            CreateCell(coordinates);
            // Operator [+] in Invector2.cs
            coordinates += MazeDirections.RandomValue.ToIntVector2();
        }
    }

    public void CreateCell(IntVector2 coordinates)
    {
        MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
        cells[coordinates.x, coordinates.z] = newCell;
        newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
        newCell.transform.parent = transform;
        newCell.transform.localPosition = new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f + 0.5f);
    }
    public MazeCell GetCell(IntVector2 coordinates)
    {
        // Get a MazeCell, for future validation in Generate()
        return cells[coordinates.x, coordinates.z];
    }
}
