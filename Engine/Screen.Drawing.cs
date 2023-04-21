namespace Engine;

static partial class Screen {
    public static void SetText(int y, int x, string message, 
                        ConsoleColor color = ConsoleColor.White, 
                        ConsoleColor backgroundColor = ConsoleColor.Black) {
        for(int ctn = 0; ctn < message.Length; ctn++) {
            if (x+ctn == 0 || x+ctn >= Width) 
                break;

            SetPixel(y, x+ctn, message[ctn], color, backgroundColor);
        }
    }
    
    public static void SetPixel(int y, int x, char symbol, 
                        ConsoleColor color = ConsoleColor.White, 
                        ConsoleColor backgroundColor = ConsoleColor.Black) {
        Buffer[y,x] = new Pixel(symbol, color, backgroundColor);
    }
    public static void SetPixel(int y, int x, Pixel pixel) {
        Buffer[y,x] = pixel;
    }
}

struct Texture {
    public int Width {get;}
    public int Height {get;}
    public Pixel[,] Buffer {get;}

    public Texture(int _height, int _width) {
        Buffer = new Pixel[_height, _width];
        Width = _width;
        Height = _height;
    }
}