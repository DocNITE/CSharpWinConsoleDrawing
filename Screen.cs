using System.Drawing;

namespace Engine;

public enum EBackgroundFillMode {
    NONE = 0,
    FILL = 1
}

public enum ERenderer {
    DEFAULT = 0, // without Colors. Only chars - Mostly faster
    ColorFUL = 1 // With Color - chars and background - So slowly
}

public partial class Screen {
    private static readonly IntPtr consoleHandle = Kernel32.GetConsoleWindow();

    public static string Title {get => Console.Title; set => Console.Title = value;}
    public static Point WindowSize {get; internal set;}
    public static Point FontSize {get; internal set;}
    public static Pixel[] Buffer {get; internal set;}
    public static Buffer ConsoleBuffer {get; internal set;}
    public static ConsoleColor BackSymbolColor;
    public static ConsoleColor BackgroundColor;
    public static ConsoleColor WindowColor;
    private static char backgroundSymbol = '#';
    private static char emptySymbol = ' ';
    private static EBackgroundFillMode wndBackFillMode;
    private static ERenderer rndMode;
    public static bool IsBorderless = false;
    /// <summary>
    /// Initialize engine screen
    /// </summary>
    /// <param name="_Width">horizontal size</param>
    /// <param name="_Height">vertical size</param>
    /// <param name="_fontWidth">font size</param>
    /// <param name="_fontHeight">font size</param>
    /// <param name="_title">Application name</param>
    /// <param name="_rndMode">Render mode (NOT USED)</param>
    public static void Initialize(int _Width, int _Height, int _fontWidth, int _fontHeight, string _title = "ConsoleEngine", ERenderer _rndMode = ERenderer.DEFAULT) {
        if (_Width < 1 || _Height < 1) throw new ArgumentOutOfRangeException();
        if (_fontWidth < 1 || _fontHeight < 1) throw new ArgumentOutOfRangeException();

        WindowSize = new(_Width, _Height);
        FontSize = new(_fontWidth, _fontHeight);
        Buffer = new Pixel[WindowSize.Y * WindowSize.X];
        BackSymbolColor = ConsoleColor.White;
        BackgroundColor = ConsoleColor.Black;
        WindowColor = ConsoleColor.White;
        wndBackFillMode = EBackgroundFillMode.NONE;
        rndMode = _rndMode;

        Console.SetWindowSize(WindowSize.X, WindowSize.Y);
        Console.SetBufferSize(WindowSize.X, WindowSize.Y);
        //SetWindowPos(consoleHandle, 5, 20, 0, WindowSize.X * FontSize.X, WindowSize.Y * FontSize.Y, 0x0040);
        Console.SetWindowPosition(0, 0);

        Console.Title = _title;
        Console.CursorVisible = false;
        // https://social.msdn.microsoft.com/Forums/vstudio/en-US/1aa43c6c-71b9-42d4-aa00-60058a85f0eb/c-console-window-disable-resize?forum=csharpgeneral
        // Disable some commands
        IntPtr handle = consoleHandle;
        IntPtr sysMenu = Kernel32.GetSystemMenu(handle, false);
        if (handle != IntPtr.Zero)
        {
            Kernel32.DeleteMenu(sysMenu, Kernel32.SC_CLOSE, Kernel32.MF_BYCOMMAND);
            Kernel32.DeleteMenu(sysMenu, Kernel32.SC_MINIMIZE, Kernel32.MF_BYCOMMAND);
            Kernel32.DeleteMenu(sysMenu, Kernel32.SC_MAXIMIZE, Kernel32.MF_BYCOMMAND);
            Kernel32.DeleteMenu(sysMenu, Kernel32.SC_SIZE, Kernel32.MF_BYCOMMAND);
        }
        // ende

        ConsoleBuffer = new Buffer(WindowSize.X, WindowSize.Y);
        Kernel32.SetConsoleMode(Input.stdInputHandle, 0x0080);
		Font.SetFont(Input.stdOutputHandle, (short)FontSize.X, (short)FontSize.Y);

        RestoreBuffer();
    }
    /// <summary>
    /// Clear screen (and Buffer)
    /// </summary>
    public static void RestoreBuffer() {
        for(int y = 0; y < WindowSize.Y; y++) {
            for(int x = 0; x < WindowSize.X; x++) {
                Buffer[GetPosition(y, x)] = new Pixel(emptySymbol, BackSymbolColor, BackgroundColor);
            }
        }
    }
    /// <summary>
    /// Draw screen from Buffer
    /// </summary>
    public static void Draw() {
        // https://stackoverflow.com/questions/2754518/how-can-i-write-fast-colored-output-to-console
        ConsoleBuffer.SetBuffer(Buffer);
        ConsoleBuffer.Blit();
        // ende
    }
    /// <summary>
    /// Set bordeless mode window
    /// (CAUTION): Not completed
    /// </summary>
    public static void Borderless() {
		IsBorderless = true;

		int GWL_STYLE = -16;                // hex konstant för stil-förändring
		int WS_BORDERLESS = 0x00080000;     // helt borderless

		Kernel32.Rect rect = new Kernel32.Rect();
		Kernel32.Rect desktopRect = new Kernel32.Rect();

		Kernel32.GetWindowRect(consoleHandle, ref rect);
		IntPtr desktopHandle = Kernel32.GetDesktopWindow();
		Kernel32.MapWindowPoints(desktopHandle, consoleHandle, ref rect, 2);
		Kernel32.GetWindowRect(desktopHandle, ref desktopRect);

		Point wPos = new Point(
			(desktopRect.Right / 2) - ((WindowSize.X * FontSize.X) / 2),
			(desktopRect.Bottom / 2) - ((WindowSize.Y * FontSize.Y) / 2));

		Kernel32.SetWindowLong(consoleHandle, GWL_STYLE, WS_BORDERLESS);
		Kernel32.SetWindowPos(consoleHandle, -2, wPos.X, wPos.Y, rect.Right - 8, rect.Bottom - 8, 0x0040);

		Kernel32.DrawMenuBar(consoleHandle);
	}
    /// <summary>
    /// Get pixel from screen
    /// </summary>
    /// <param name="y">vertical coord</param>
    /// <param name="x">horizontal coord</param>
    /// <returns></returns>
    public static Pixel? GetPixel(int y, int x) {
        var formule = WindowSize.X * y + x;
        if (formule >= WindowSize.X * WindowSize.Y) 
            return null;

        return Buffer[formule];
    }
    /// <summary>
    /// Convert point to linear position for buffer
    /// </summary>
    /// <param name="y">vertical</param>
    /// <param name="x">horizontal</param>
    /// <returns></returns>
    public static int GetPosition(int y, int x) {
        return WindowSize.X * y + x;
    }
}

public struct Pixel {
    public char Symbol;
    public ConsoleColor Color;
    public ConsoleColor BackgroundColor;

    public Pixel(
                    char _symbol, 
                    ConsoleColor _Color = ConsoleColor.White, 
                    ConsoleColor _bColor = ConsoleColor.Black) 
    {
        Symbol = _symbol;
        Color = _Color;
        BackgroundColor = _bColor;
    }
}