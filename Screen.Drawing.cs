using System;

namespace Engine;

public partial class Screen {
    /// <summary>
    /// Drawing with text line
    /// </summary>
    /// <param name="y">Vertical position</param>
    /// <param name="x">Horizontal position</param>
    /// <param name="message">Text line</param>
    /// <param name="color">Text color</param>
    /// <param name="backgroundColor">Background box color</param>
    public static void SetText(int y, int x, string message, 
                        ConsoleColor color = ConsoleColor.White, 
                        ConsoleColor backgroundColor = ConsoleColor.Black) {
        for(int ctn = 0; ctn < message.Length; ctn++) {
            if (x+ctn < 0 || x+ctn >= WindowSize.X) 
                break;

            SetPixel(y, x+ctn, message[ctn], color, backgroundColor);
        }
    }
    /// <summary>
    /// Drawing char symbol like screen pixel
    /// </summary>
    /// <param name="y">Vertical position</param>
    /// <param name="x">Horizontal position</param>
    /// <param name="symbol">Char symbol</param>
    /// <param name="color">Symbol color</param>
    /// <param name="backgroundColor">Background box color</param>
    public static void SetPixel(int y, int x, char symbol, 
                        ConsoleColor color = ConsoleColor.White, 
                        ConsoleColor backgroundColor = ConsoleColor.Black) {
        if (y >= WindowSize.Y || x >= WindowSize.X || y < 0 || x < 0) 
            return;

        Buffer[GetPosition(y, x)] = new Pixel(symbol, color, backgroundColor);
    }
    /// <summary>
    /// Drawing char symbol like screen pixel
    /// </summary>
    /// <param name="y">Vertical position</param>
    /// <param name="x">Horizontal position</param>
    /// <param name="pixel">Pixel what be drawen</param>
    public static void SetPixel(int y, int x, Pixel pixel) {
        if (y >= WindowSize.Y || x >= WindowSize.X || y < 0 || x < 0) 
            return;

        Buffer[GetPosition(y, x)] = pixel;
    }

    public static void Triangle(Point a, Point b, Point c, int color, char character = 'X') {
		Triangle(a,b,c,color,0,character);
	}

	/// <summary> Draws a Triangle. </summary>
	/// <param name="a">Point A.</param>
	/// <param name="b">Point B.</param>
	/// <param name="c">Point C.</param>
	/// <param name="fgColor">Color to draw with.</param>
	/// <param name="bgColor">Color to draw to the background with.</param>
	/// <param name="character">Character to use.</param>
	public static void Triangle(Point a, Point b, Point c, int fgColor,int bgColor, char character = 'X')
	{
		Line(a, b, fgColor,bgColor, character);
		Line(b, c, fgColor, bgColor, character);
		Line(c, a, fgColor, bgColor, character);
	}

	// Bresenhams Triangle Algorithm

	/// <summary> Draws a Triangle and fills it, calls overloaded method with background as bgColor </summary>
	/// <param name="a">Point A.</param>
	/// <param name="b">Point B.</param>
	/// <param name="c">Point C.</param>
	/// <param name="color">Color to draw with.</param>
	/// <param name="character">Character to use.</param>
	public static void FillTriangle(Point a, Point b, Point c, int color, char character = 'X') {
		FillTriangle(a, b, c, color, 0, character);
	}

	/// <summary> Draws a Triangle and fills it. </summary>
	/// <param name="a">Point A.</param>
	/// <param name="b">Point B.</param>
	/// <param name="c">Point C.</param>
	/// <param name="fgColor">Color to draw with.</param>
	/// <param name="bgColor">Color to draw to the background with.</param>
	/// <param name="character">Character to use.</param>
	public static void FillTriangle(Point a, Point b, Point c, int fgColor, int bgColor, char character = 'X')
	{
		Point min = new Point(Math.Min(Math.Min(a.X, b.X), c.X), Math.Min(Math.Min(a.Y, b.Y), c.Y));
		Point max = new Point(Math.Max(Math.Max(a.X, b.X), c.X), Math.Max(Math.Max(a.Y, b.Y), c.Y));

		Point p = new Point();
		for(p.Y = min.Y; p.Y < max.Y; p.Y++) {
			for(p.X = min.X; p.X < max.X; p.X++) {
				int w0 = Orient(b, c, p);
				int w1 = Orient(c, a, p);
				int w2 = Orient(a, b, p);

				if(w0 >= 0 && w1 >= 0 && w2 >= 0) SetPixel(p.Y, p.X, character, (ConsoleColor)fgColor, (ConsoleColor)bgColor);
			}
		}
	}

	private static int Orient(Point a, Point b, Point c) {
		return ((b.X - a.X) * (c.Y - a.Y)) - ((b.Y - a.Y) * (c.X - a.X));
	}

    // Bresenhams Line Algorithm
	// https://en.wikipedia.org/wiki/Bresenham%27s_line_algorithm
	/// <summary> Draws a line from start to end. (Bresenhams Line), calls overloaded method with background as bgColor </summary>
	/// <param name="start">Point to draw line from.</param>
	/// <param name="end">Point to end line at.</param>
	/// <param name="color">Color to draw with.</param>
	/// <param name="c">Character to use.</param>
	public static void Line(Point start, Point end, int color, char c = 'X') {
		Line(start, end, color, 0, c);
	}

	// Bresenhams Line Algorithm
	// https://en.wikipedia.org/wiki/Bresenham%27s_line_algorithm
	/// <summary> Draws a line from start to end. (Bresenhams Line) </summary>
	/// <param name="start">Point to draw line from.</param>
	/// <param name="end">Point to end line at.</param>
	/// <param name="fgColor">Color to draw with.</param>
	/// <param name="bgColor">Color to draw the background with.</param>
	/// <param name="c">Character to use.</param>
	public static void Line(Point start, Point end, int fgColor,int bgColor, char c = 'X')
	{
		Point delta = end - start;
		Point da = Point.Zero, db = Point.Zero;
		if(delta.X < 0) da.X = -1; else if(delta.X > 0) da.X = 1;
		if(delta.Y < 0) da.Y = -1; else if(delta.Y > 0) da.Y = 1;
		if(delta.X < 0) db.X = -1; else if(delta.X > 0) db.X = 1;
		int longest = Math.Abs(delta.X);
		int shortest = Math.Abs(delta.Y);

		if(!(longest > shortest)) {
			longest = Math.Abs(delta.Y);
			shortest = Math.Abs(delta.X);
			if(delta.Y < 0) db.Y = -1; else if(delta.Y > 0) db.Y = 1;
			db.X = 0;
		}

		int numerator = longest >> 1;
		Point p = new Point(start.X, start.Y);
		for(int i = 0; i <= longest; i++) {
			SetPixel(p.Y, p.X, c, (ConsoleColor)fgColor, (ConsoleColor)bgColor);
			numerator += shortest;
			if(!(numerator < longest)) {
				numerator -= longest;
				p += da;
			}
			else {
				p += db;
			}
		}
	}
}

public struct Texture {
    public int Width {get;}
    public int Height {get;}
    public Pixel[] Buffer {get;}

    public Texture(int _height, int _width) {
        Buffer = new Pixel[_height * _width];
        Width = _width;
        Height = _height;
    }
}

public class Color {
	/// <summary> Red component. </summary>
	public uint R { get; set; }
	/// <summary> Green component. </summary>
	public uint G { get; set; }
	/// <summary> Bkue component. </summary>
	public uint B { get; set; }
	/// <summary> Creates a new Color from rgb. </summary>
	public Color(int r, int g, int b) {
		this.R = (uint)r;
		this.G = (uint)g;
		this.B = (uint)b;
	}
}