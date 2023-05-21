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

    public static void Initialize(int _Width, int _Height, string _title = "ConsoleEngine", ERenderer _rndMode = ERenderer.DEFAULT) {
        Width = _Width;
        Height = _Height;
        Buffer = new Pixel[Height * Width];
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
        //Console.SetCursorPosition(0,0);
//
        //if (rndMode == ERenderer.DEFAULT) {
        //    string symbols = "";
        //    for(int y = 0; y < Height; y++) {
        //        for(int x = 0; x < Width; x++) {
        //            symbols = symbols + Buffer[y, x].Symbol;
        //        }
        //        
        //        symbols = symbols + "\n";
        //    }
        //    Console.Write(symbols);
        //} else if (rndMode == ERenderer.ColorFUL) {
        //    for(int y = 0; y < Height; y++) {
        //        for(int x = 0; x < Width; x++) {
        //            Console.BackgroundColor = Buffer[y, x].BackgroundColor;
        //            Console.ForegroundColor = Buffer[y, x].Color;
        //            Console.Write(Buffer[y, x].Symbol);
        //        }
        //        
        //        if (wndBackFillMode == EBackgroundFillMode.NONE)
        //            Console.BackgroundColor = ConsoleColor.Black; 
        //        
        //        Console.Write("\n");
        //    }
        //}

        if (!sf_handler.IsInvalid)
        {
            CharInfo[] buf = new CharInfo[Width * Height];
            SmallRect rect = new SmallRect() { Left = 0, Top = 0, Right = (short)Width, Bottom = (short)Height };

            for (int i = 0; i < buf.Length; ++i)
            {
                buf[i].Attributes = 4;
                buf[i].Char.UnicodeChar = Buffer[i].Symbol;
            }

            bool b = WriteConsoleOutputW(sf_handler, buf,
              new Coord() { X = (short)Width, Y = (short)Height },
              new Coord() { X = 0, Y = 0 },
              ref rect);
            //for (ushort character = 0x2551; character < 0x2551 + 26; ++character)
            //{
            //    for (short attribute = 0; attribute < 15; ++attribute)
            //    {
            //        for (int i = 0; i < buf.Length; ++i)
            //        {
            //            buf[i].Attributes = attribute;
            //            buf[i].Char.UnicodeChar = character;
            //        }
//
            //        bool b = WriteConsoleOutputW(sf_handler, buf,
            //          new Coord() { X = (short)Width, Y = (short)Height },
            //          new Coord() { X = 0, Y = 0 },
            //          ref rect);
            //    }
            //}
        }
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