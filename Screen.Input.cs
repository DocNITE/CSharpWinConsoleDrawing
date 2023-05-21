using System.Drawing;

namespace Engine;

public partial class Screen {
    private static readonly IntPtr stdInputHandle = GetStdHandle(-10);
	private static readonly IntPtr stdOutputHandle = GetStdHandle(-11);
	private static readonly IntPtr stdErrorHandle = GetStdHandle(-12);
	private static readonly IntPtr consoleHandle = GetConsoleWindow();

    /// <summary>Checks to see if the console is in focus </summary>
	/// <returns>True if Console is in focus</returns>
	private static bool ConsoleFocused()
	{
		return GetConsoleWindow() == GetForegroundWindow();
	}

    /// <summary> Checks if specified key is pressed. </summary>
	/// <param name="key">The key to check.</param>
	/// <returns>True if key is pressed</returns>
	public static bool GetKey(ConsoleKey key) {
		short s = GetAsyncKeyState((int)key);
		return (s & 0x8000) > 0 && ConsoleFocused();
	}

	/// <summary> Checks if specified keyCode is pressed. </summary>
	/// <param name="virtualkeyCode">keycode to check</param>
	/// <returns>True if key is pressed</returns>
	public static bool GetKey(int virtualkeyCode)
	{
		short s = GetAsyncKeyState(virtualkeyCode);
		return (s & 0x8000) > 0 && ConsoleFocused();
	}

	/// <summary> Checks if specified key is pressed down. </summary>
	/// <param name="key">The key to check.</param>
	/// <returns>True if key is down</returns>
	public static bool GetKeyDown(ConsoleKey key) {
		int s = Convert.ToInt32(GetAsyncKeyState((int)key));
		return (s == -32767) && ConsoleFocused();
	}

	/// <summary> Checks if specified keyCode is pressed down. </summary>
	/// <param name="virtualkeyCode">keycode to check</param>
	/// <returns>True if key is down</returns>
	public static bool GetKeyDown(int virtualkeyCode)
	{
		int s = Convert.ToInt32(GetAsyncKeyState(virtualkeyCode));
		return (s == -32767) && ConsoleFocused();
	}

	/// <summary> Checks if left mouse button is pressed down. </summary>
	/// <returns>True if left mouse button is down</returns>
	public static bool GetMouseLeft() {
		short s = GetAsyncKeyState(0x01);
		return (s & 0x8000) > 0 && ConsoleFocused();
	}

	/// <summary> Checks if right mouse button is pressed down. </summary>
	/// <returns>True if right mouse button is down</returns>
	public static bool GetMouseRight()
	{
		short s = GetAsyncKeyState(0x02);
		return (s & 0x8000) > 0 && ConsoleFocused();
	}

	/// <summary> Checks if middle mouse button is pressed down. </summary>
	/// <returns>True if middle mouse button is down</returns>
	public static bool GetMouseMiddle()
	{
		short s = GetAsyncKeyState(0x04);
		return (s & 0x8000) > 0 && ConsoleFocused();
	}

	/// <summary> Gets the mouse position. </summary>
	/// <returns>The mouse's position in character-space.</returns>
	/// <exception cref="Exception"/>
	//public static Point GetMousePos() {
	//	Rect r = new Rect();
	//	GetWindowRect(GetConsoleWindow(), ref r);
//
	//	if (GetCursorPos(out Point p)) {
	//		Point point = new Point();
	//		if (!IsBorderless) {
	//			p.Y -= 29;
	//			point = new Point(
	//				(int)Math.Floor(((p.X - r.Left) / (float)FontSize.X) - 0.5f),
	//				(int)Math.Floor(((p.Y - r.Top) / (float)FontSize.Y))
	//			);
	//		} else {
	//			point = new Point(
	//				(int)Math.Floor(((p.X - r.Left) / (float)FontSize.X)),
	//				(int)Math.Floor(((p.Y - r.Top) / (float)FontSize.Y))
	//			);
	//		}
	//		return new Point(Utility.Clamp(point.X, 0, WindowSize.X - 1), Utility.Clamp(point.Y, 0, WindowSize.Y - 1));
	//	}
	//	throw new Exception();
	//}
}