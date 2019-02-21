using UnityEngine;

public class MazeCell : MonoBehaviour {

	public IntVector2 coordinates;

	private MazeCellEdge[] edges = new MazeCellEdge[MazeDirections.Count];

	public MazeCellEdge GetEdge (MazeDirection direction) {
		return edges[(int)direction];
	}

    private int initializedEdgeCount;

    public bool IsFullyInitialized // A cell is fully initialized when its all 4 walls are created.
    {
        get
        {
            return initializedEdgeCount == MazeDirections.Count;
        }
    }

	public void SetEdge (MazeDirection direction, MazeCellEdge edge) {
		edges[(int)direction] = edge;
        // Increase count whenever a wall is set up
        initializedEdgeCount += 1;
	}

    public MazeDirection RandomUninitializedDirection
    {
        get
        {
            int skips = Random.Range(0, MazeDirections.Count - initializedEdgeCount);
            for (int i = 0; i < MazeDirections.Count; i++)
            {
                if (edges[i] == null)
                {
                    if (skips == 0)
                    {
                        return (MazeDirection)i;
                    }
                    skips -= 1;
                }
            }
            throw new System.InvalidOperationException("Maze cell has no uninitialized directions left.");
        }
    }
}