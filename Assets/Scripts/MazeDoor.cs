using UnityEngine;

public class MazeDoor : MazePassage {

	public Transform hinge;

    private static Quaternion
    normalRotation = Quaternion.Euler(0f, -90f, 0f),
    mirroredRotation = Quaternion.Euler(0f, 90f, 0f);

    private bool isMirrored;

    private MazeDoor OtherSideOfDoor {
		get {
			return otherCell.GetEdge(direction.GetOpposite()) as MazeDoor;
		}
	}
	
	public override void Initialize (MazeCell primary, MazeCell other, MazeDirection direction) {
		base.Initialize(primary, other, direction);
        if (OtherSideOfDoor != null) {
            isMirrored = true;
            hinge.localScale = new Vector3(-1f, 1f, 1f);
			Vector3 p = hinge.localPosition;
			p.x = -p.x;
			hinge.localPosition = p;
		}
	}

    public override void OnPlayerEntered()
    {
        OtherSideOfDoor.hinge.localRotation = hinge.localRotation = 
            isMirrored ? mirroredRotation : normalRotation; ;
    }

    public override void OnPlayerExited()
    {
        OtherSideOfDoor.hinge.localRotation = hinge.localRotation = Quaternion.identity;
    }
}