using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MazeDirection
{
    North,
    East,
    South,
    West
}

public static class MazeDirections
{
    public const int Count = 4; // 4 directions: N, S, W, E
    public static MazeDirection RandomValue
    {
        get
        {
            // Randomly selection 1 out of 4 directions
            return (MazeDirection)Random.Range(0, Count);
        }
    }

    private static IntVector2[] vectors =
    {
        // These present 4 possibilities when the drawing can go up/down/left/right
        new IntVector2(0,1),
        new IntVector2(1,0),
        new IntVector2(0,-1),
        new IntVector2(-1,0)
    };

    public static IntVector2 ToIntVector2 (this MazeDirection direction)
    {
        // Return an IntVector2 in a random direction, (not just by incresing either x or z)
        return vectors[(int)direction];
    }
}
