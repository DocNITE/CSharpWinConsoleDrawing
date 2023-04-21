namespace Engine;

enum EBackgroundFillMode {
    NONE = 0,
    FILL = 1
}

enum ERenderer {
    DEFAULT = 0, // without Colors. Only chars - Mostly faster
    ColorFUL = 1 // With Color - chars and background - So slowly
}

static partial class Screen {
    public static int Width {get; internal set;}
    public static int Height {get; internal set;}
    public static Pixel[,] Buffer {get; internal set;}
    public static ConsoleColor BackSymbolColor;
    public static ConsoleColor BackgroundColor;
    public static ConsoleColor WindowColor;
    private static char backgroundSymbol = '#';
    private static char emptySymbol = ' ';
    private static EBackgroundFillMode wndBackFillMode;
    private static ERenderer rndMode;

    public static void Initialize(int _Width, int _Height, string _title = "ConsoleEngine", ERenderer _rndMode = ERenderer.DEFAULT) {
        Width = _Width;
        Height = _Height;
        Buffer = new Pixel[Height, Width];
        BackSymbolColor = ConsoleColor.White;
        BackgroundColor = ConsoleColor.Black;
        WindowColor = ConsoleColor.White;
        wndBackFillMode = EBackgroundFillMode.NONE;
        rndMode = _rndMode;

        Console.SetWindowSize(Width+2, Height+2);
        Console.SetBufferSize(Width+2, Height+2);
        Console.SetWindowPosition(0, 0);

        Console.Title = _title;
        Console.CursorVisible = false;

        Console.Beep();

        RestoreBuffer();
    }

    public static void RestoreBuffer() {
        for(int y = 0; y < Height; y++) {
            for(int x = 0; x < Width; x++) {
                if (y == 0 || y == Height-1) {
                    Buffer[y,x] = new Pixel(backgroundSymbol, BackSymbolColor, WindowColor);
                } else if (x == 0 || x == Width-1) {
                    Buffer[y,x] = new Pixel(backgroundSymbol, BackSymbolColor, WindowColor);
                } else {
                    Buffer[y,x] = new Pixel(emptySymbol, BackSymbolColor, BackgroundColor);
                }
            }
        }
    }

    public static void Draw() {
        Console.SetCursorPosition(0,0);

        if (rndMode == ERenderer.DEFAULT) {
            string symbols = "";
            for(int y = 0; y < Height; y++) {
                for(int x = 0; x < Width; x++) {
                    symbols = symbols + Buffer[y, x].Symbol;
                }
                
                symbols = symbols + "\n";
            }
            Console.Write(symbols);
        } else if (rndMode == ERenderer.ColorFUL) {
            for(int y = 0; y < Height; y++) {
                for(int x = 0; x < Width; x++) {
                    Console.BackgroundColor = Buffer[y, x].BackgroundColor;
                    Console.ForegroundColor = Buffer[y, x].Color;
                    Console.Write(Buffer[y, x].Symbol);
                }
                
                if (wndBackFillMode == EBackgroundFillMode.NONE)
                    Console.BackgroundColor = ConsoleColor.Black; 
                
                Console.Write("\n");
            }
        }
    }
}

struct Pixel {
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

// NOT USED!
/*
struct RGB {
    public int r;
    public int g;
    public int b;

    public RGB(int _r = 255, int _g = 255, int _b = 255) {
        r = _r;
        g = _g;
        b = _b;
    } 
}
*/