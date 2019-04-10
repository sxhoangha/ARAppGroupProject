using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    private MazeCell currentCell;

    private MazeDirection currentDirection;

    private float scale;

    private float moveSpeed = 1;

    private void Start()
    {
        scale = PlayerPrefs.GetFloat("Scale");
        if (scale == 0)
        {
            scale = 0.05f;
        }
    }

    private void Look(MazeDirection direction)
    {
        transform.localRotation = direction.ToRotation();
        currentDirection = direction;
    }

    public void SetLocation(MazeCell cell)
    {
        if (currentCell != null)
        {
            currentCell.OnPlayerExited();
        }
        currentCell = cell;
        Vector3 localPosition = cell.transform.localPosition;
        transform.localPosition = new Vector3(localPosition.x, scale * 0.3f, localPosition.z);
        currentCell.OnPlayerEntered();
    }

    public void Move(MazeDirection direction)
    {
        MazeCellEdge edge = currentCell.GetEdge(direction);
        if (edge is MazePassage)
        {
            SetLocation(edge.otherCell);
        }
    }
    public void MoveMove(MazeDirection direction)
    {
        MazeCellEdge edge = currentCell.GetEdge(direction);
        if (edge is MazePassage)
        {
            SetLocation(edge.otherCell);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(MazeDirection.North);
            Look(MazeDirection.North);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(MazeDirection.East);
            Look(MazeDirection.East);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(MazeDirection.South);
            Look(MazeDirection.South);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(MazeDirection.West);
            Look(MazeDirection.West);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            Look(currentDirection.GetNextCounterclockwise());
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Look(currentDirection.GetNextClockwise());
        }

        // for buttons on UI
        if (Input.GetMouseButtonDown(0) && EventSystem.current.currentSelectedGameObject != null)
        {
            if (EventSystem.current.currentSelectedGameObject.name == "ButtonNorth")
            {
                Move(MazeDirection.North);
                Look(MazeDirection.North);
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "ButtonEast")
            {
                Move(MazeDirection.East);
                Look(MazeDirection.East);
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "ButtonSouth")
            {
                Move(MazeDirection.South);
                Look(MazeDirection.South);
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "ButtonWest")
            {
                Move(MazeDirection.West);
                Look(MazeDirection.West);
            }

            //else if (EventSystem.current.currentSelectedGameObject.name == "ButtonTurnLeft")
            //{
            //    Look(currentDirection.GetNextCounterclockwise());
            //}

            //else if (EventSystem.current.currentSelectedGameObject.name == "ButtonTurnRight")
            //{
            //    Look(currentDirection.GetNextClockwise());
            //}
        }

    }
}