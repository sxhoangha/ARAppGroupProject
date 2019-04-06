[System.Serializable]
// This script creates a new type of Vector2 that contians Integer insteads of Floats
// Usually, Vector2 has 2 float parameters, but in this game we need Integers instead
public struct IntVector2 {

	public int x, z;

	public IntVector2 (int x, int z) {
		this.x = x;
		this.z = z;
	}

	public static IntVector2 operator + (IntVector2 a, IntVector2 b) {
		a.x += b.x;
		a.z += b.z;
		return a;
	}
}