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
        Buffer[GetPosition(y, x)] = new Pixel(symbol, color, backgroundColor);
    }
    /// <summary>
    /// Drawing char symbol like screen pixel
    /// </summary>
    /// <param name="y">Vertical position</param>
    /// <param name="x">Horizontal position</param>
    /// <param name="pixel">Pixel what be drawen</param>
    public static void SetPixel(int y, int x, Pixel pixel) {
        Buffer[GetPosition(y, x)] = pixel;
    }
}

public struct Texture {
    public int Width {get;}
    public int Height {get;}
    public Pixel[,] Buffer {get;}

    public Texture(int _height, int _width) {
        Buffer = new Pixel[_height, _width];
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