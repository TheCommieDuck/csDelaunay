using System;

// Recreation of the UnityEngine.Vector3, so it can be used in other thread
public struct Vector2f {
	
	public float X, Y;
	
	public static readonly Vector2f zero = new Vector2f(0,0);
	public static readonly Vector2f one = new Vector2f(1,1);

	public static readonly Vector2f right = new Vector2f(1,0);
	public static readonly Vector2f left = new Vector2f(-1,0);
	
	public static readonly Vector2f up = new Vector2f(0,1);
	public static readonly Vector2f down = new Vector2f(0,-1);
	
	public Vector2f(float x, float y) {
		this.X = x;
		this.Y = y;
	}
	public Vector2f(double x, double y) {
		this.X = (float)x;
		this.Y = (float)y;
	}
	
	public float magnitude {
		get{
			return (float)Math.Sqrt(X*X + Y*Y);
		}
	}

	public void Normalize() {
		float magnitude = this.magnitude;
		X /= magnitude;
		Y /= magnitude;
	}

	public static Vector2f Normalize(Vector2f a) {
		float magnitude = a.magnitude;
		return new Vector2f(a.X/magnitude, a.Y/magnitude);
	}
	
	public override bool Equals(object other) {
		if (!(other is Vector2f)) {
			return false;
		}
		Vector2f v = (Vector2f) other;
		return X == v.X &&
			Y == v.Y;
	}
	
	public override string ToString () {
		return string.Format ("[Vector2f]"+X+","+Y);
	}
	
	public override int GetHashCode () {
		return X.GetHashCode () ^ Y.GetHashCode () << 2;
	}

	public float DistanceSquare(Vector2f v) {
		return Vector2f.DistanceSquare(this, v);
	}
	public static float DistanceSquare(Vector2f a, Vector2f b) {
		float cx = b.X - a.X;
		float cy = b.Y - a.Y;
		return cx*cx + cy*cy;
	}
	
	public static bool operator == (Vector2f a, Vector2f b) {
		return a.X == b.X && 
			a.Y == b.Y;
	}
	
	public static bool operator != (Vector2f a, Vector2f b) {
		return a.X != b.X ||
			a.Y != b.Y;
	}
	
	public static Vector2f operator - (Vector2f a, Vector2f b) {
		return new Vector2f( a.X-b.X, a.Y-b.Y);
	}
	
	public static Vector2f operator + (Vector2f a, Vector2f b) {
		return new Vector2f( a.X+b.X, a.Y+b.Y);
	}

	public static Vector2f operator * (Vector2f a, int i) {
		return new Vector2f(a.X*i, a.Y*i);
	}
	
	public static Vector2f Min(Vector2f a, Vector2f b) {
		return new Vector2f(Math.Min(a.X, b.X), Math.Min(a.Y, b.Y));
	}
	public static Vector2f Max(Vector2f a, Vector2f b) {
		return new Vector2f(Math.Max(a.X, b.X), Math.Max(a.Y, b.Y));
	}
}
