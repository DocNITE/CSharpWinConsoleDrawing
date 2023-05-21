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
    public static string Title {get => Console.Title; set => Console.Title = value;}
    public static int Width {get; internal set;}
    public static int Height {get; internal set;}
    public static Pixel[] Buffer {get; internal set;}
    public static ConsoleColor BackSymbolColor;
    public static ConsoleColor BackgroundColor;
    public static ConsoleColor WindowColor;
    private static char backgroundSymbol = '#';
    private static char emptySymbol = ' ';
    private static EBackgroundFillMode wndBackFillMode;
    private static ERenderer rndMode;
    public static bool IsBorderless = false;

    public static void Initialize(int _Width, int _Height, string _title = "ConsoleEngine", ERenderer _rndMode = ERenderer.DEFAULT) {
        Width = _Width;
        Height = _Height;
        Buffer = new Pixel[Height * Width];
        BackSymbolColor = ConsoleColor.White;
        BackgroundColor = ConsoleColor.Black;
        WindowColor = ConsoleColor.White;
        wndBackFillMode = EBackgroundFillMode.NONE;
        rndMode = _rndMode;

        //Console.SetWindowSize(Width, Height);
        Console.SetBufferSize(160, 90);
        SetWindowPos(consoleHandle, 5, 20, 0, 1300, 700, 0x0040);

        Console.Title = _title;
        Console.CursorVisible = false;
        // https://social.msdn.microsoft.com/Forums/vstudio/en-US/1aa43c6c-71b9-42d4-aa00-60058a85f0eb/c-console-window-disable-resize?forum=csharpgeneral
        // Disable some commands
        IntPtr handle = consoleHandle;
        IntPtr sysMenu = GetSystemMenu(handle, false);
        if (handle != IntPtr.Zero)
        {
            //DeleteMenu(sysMenu, SC_CLOSE, MF_BYCOMMAND);
            //DeleteMenu(sysMenu, SC_MINIMIZE, MF_BYCOMMAND);
            //DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND);
            //DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND);
        }
        // ende

        SetConsoleMode(stdInputHandle, 0x0080);
		Font.SetFont(stdOutputHandle, (short)10, (short)10);

        RestoreBuffer();
    }

    public static void RestoreBuffer() {
        for(int y = 0; y < Height; y++) {
            for(int x = 0; x < Width; x++) {
                //if (y == 0 || y == Height-1) {
                //    Buffer[y,x] = new Pixel(backgroundSymbol, BackSymbolColor, WindowColor);
                //} else if (x == 0 || x == Width-1) {
                //    Buffer[y,x] = new Pixel(backgroundSymbol, BackSymbolColor, WindowColor);
                //} else {
                //    Buffer[y,x] = new Pixel(emptySymbol, BackSymbolColor, BackgroundColor);
                //}
                Buffer[GetPosition(y, x)] = new Pixel(emptySymbol, BackSymbolColor, BackgroundColor);
            }
        }
    }

    public static void Draw() {
        Console.SetCursorPosition(0,0);
        // https://stackoverflow.com/questions/2754518/how-can-i-write-fast-colored-output-to-console
        if (!sf_handler.IsInvalid)
        {
            CharInfo[] buf = new CharInfo[Width * Height];
            Rect rect = new Rect() { Left = 0, Top = 0, Right = (short)Width, Bottom = (short)Height };

            for (int i = 0; i < buf.Length; ++i)
            {
                buf[i].Attributes = (short)(Buffer[i].Color | (Buffer[i].BackgroundColor) ); //(short)(GlyphBuffer[x, y].fg |(GlyphBuffer[x,y].bg << 4) )
                buf[i].Char.UnicodeChar = Buffer[i].Symbol;
            }

            bool b = WriteConsoleOutputW(sf_handler, buf,
              new Coord() { X = (short)Width, Y = (short)Height },
              new Coord() { X = 0, Y = 0 },
              ref rect);
        }
        // ende
    }

    public static Pixel? GetPixel(int y, int x) {
        var formule = Width * y + x;
        if (formule >= Width * Height) 
            return null;

        return Buffer[formule];
    }

    public static int GetPosition(int y, int x) {
        return Width * y + x;
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