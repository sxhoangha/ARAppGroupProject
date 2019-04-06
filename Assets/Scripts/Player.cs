using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour {

    private MazeCell currentCell;

    private MazeDirection currentDirection;

    private void Look(MazeDirection direction)
    {
        transform.localRotation = direction.ToRotation();
        currentDirection = direction;
    }

    public void SetLocation(MazeCell cell) {
        if (currentCell != null)
        {
            currentCell.OnPlayerExited();
        }
        currentCell = cell;
        transform.localPosition = cell.transform.localPosition;
        currentCell.OnPlayerEntered();
    }

    public void Move(MazeDirection direction) {
        MazeCellEdge edge = currentCell.GetEdge(direction);
        if (edge is MazePassage) {
            SetLocation(edge.otherCell);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(currentDirection);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(currentDirection.GetNextClockwise());
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(currentDirection.GetOpposite());
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(currentDirection.GetNextCounterclockwise());
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
                Move(currentDirection);
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "ButtonEast")
            {
                Move(currentDirection.GetNextClockwise());
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "ButtonSouth")
            {
                Move(currentDirection.GetOpposite());
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "ButtonWest")
            {
                Move(currentDirection.GetNextCounterclockwise());
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