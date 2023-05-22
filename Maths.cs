namespace Engine;

public struct Point {
	public int X { get; set; }
	public int Y { get; set; }
	public const float Rad2Deg = 180f / (float)Math.PI;
	public const float Deg2Rad = (float)Math.PI / 180f;
	/// <summary> new Point(0, 0); </summary>
	public static Point Zero { get; private set; } = new Point(0, 0);
	public Point(int x, int y) {
		this.X = x;
		this.Y = y;
	}
	public Vector ToVector() => new Vector((float)X, (float)Y);
	public override string ToString() => String.Format("({0}, {1})", X, Y);
	public static Point operator +(Point a, Point b) {
		return new Point(a.X + b.X, a.Y + b.Y);
	}
	public static Point operator -(Point a, Point b) {
		return new Point(a.X - b.X, a.Y - b.Y);
	}
	public static Point operator +(Point a, int b)
	{
		return new Point(a.X + b, a.Y + b);
	}
	public static Point operator -(Point a, int b)
	{
		return new Point(a.X - b, a.Y - b);
	}
	public static Point operator /(Point a, float b) {
		return new Point((int)(a.X / b), (int)(a.Y / b));
	}
	public static Point operator *(Point a, float b) {
		return new Point((int)(a.X * b), (int)(a.Y * b));
	}
	public static bool operator ==(Point a, Point b)
	{
		return a.X == b.X && a.Y == b.Y;
	}
	public static bool operator !=(Point a, Point b)
	{
		return a.X != b.X || a.Y != b.Y;
	}
	public static bool operator <=(Point a, Point b)
	{
		return a.X <= b.X && a.Y <= b.Y;
	}
	public static bool operator >=(Point a, Point b)
	{
		return a.X >= b.X && a.Y >= b.Y;
	}
	public static bool operator <(Point a, Point b)
	{
		return a.X < b.X && a.Y < b.Y;
	}
	public static bool operator >(Point a, Point b)
	{
		return a.X > b.X && a.Y > b.Y;
	}
	public override bool Equals(object obj) {
		return Equals(obj);
	}
	public override int GetHashCode() {
		return GetHashCode();
	}
	/// <summary> Calculates distance between two points. </summary>
	/// <param name="a">Point A</param>
	/// <param name="b">Point B</param>
	/// <returns>Distance between A and B</returns>
	public static float Distance(Point a, Point b) {
		Point dV = b - a;
		float d = (float)Math.Sqrt(Math.Pow(dV.X, 2) + Math.Pow(dV.Y, 2));
		return d;
	}
	public void Clamp(Point min, Point max) {
		X = (X > max.X) ? max.X : X;
		X = (X < min.X) ? min.X : X;
		Y = (Y > max.Y) ? max.Y : Y;
		Y = (Y < min.Y) ? min.Y : Y;
	}
}

public struct Vector {
	public float X { get; set; }
	public float Y { get; set; }
	public static Vector Zero { get; private set; } = new Vector(0, 0);
	public Vector(float x, float y) {
		this.X = x;
		this.Y = y;
	}
	public Point ToPoint => new Point((int)Math.Round(X, 0), (int)Math.Round(Y, 0));
	public void Rotate(float a) {
		Vector n = Vector.Zero;
		n.X = (float)(X * Math.Cos(a / 57.3f) - Y * Math.Sin(a / 57.3f));
		n.Y = (float)(X * Math.Sin(a / 57.3f) + Y * Math.Cos(a / 57.3f));
		X = n.X;
		Y = n.Y;
	}
	public static Vector operator + (Vector a, Vector b) {
		return new Vector(a.X + b.X, a.Y + b.Y);
	}
	public static Vector operator - (Vector a, Vector b) {
		return new Vector(a.X - b.X, a.Y - b.Y);
	}
	public static Vector operator / (Vector a, float b) {
		return new Vector((a.X / b), (a.Y / b));
	}
	public static Vector operator * (Vector a, float b) {
		return new Vector((a.X * b), (a.Y * b));
	}
	public static float Distance(Vector a, Vector b) {
		Vector dV = b - a;
		float d = (float)Math.Sqrt(Math.Pow(dV.X, 2) + Math.Pow(dV.Y, 2));
		return d;
	}
}
