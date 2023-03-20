using System;

namespace Engine;

enum EBackgroundFillMode {
    NONE = 0,
    FILL = 1
}

enum ERenderer {
    DEFAULT = 0, // without colors. Only chars - Mostly faster
    COLORFUL = 1 // With color - chars and background - So slowly
}

struct VirtualScreen {
    public readonly int width;
    public readonly int height;
    public readonly VirtualPixel[,] buffer;
    public ConsoleColor backgroundSymbolColor;
    public ConsoleColor backgroundColor;
    public ConsoleColor windowColor;
    public char backgroundSymbol = '#';
    public char emptySymbol = ' ';
    public EBackgroundFillMode wndBackFillMode;
    public ERenderer rndMode;

    public VirtualScreen(int _width, int _height, ERenderer _rndMode = ERenderer.DEFAULT) {
        width = _width;
        height = _height;
        buffer = new VirtualPixel[height, width];
        backgroundSymbolColor = ConsoleColor.White;
        backgroundColor = ConsoleColor.Black;
        windowColor = ConsoleColor.White;
        wndBackFillMode = EBackgroundFillMode.NONE;

        Console.SetWindowSize(width+2, height+2);
        Console.SetBufferSize(width+2, height+2);

        Console.Beep();

        RestoreBuffer();
    }

    public void SetText(int y, int x, string message, 
                        ConsoleColor color = ConsoleColor.White, 
                        ConsoleColor backgroundColor = ConsoleColor.Black) {
        for(int ctn = 0; ctn < message.Length; ctn++) {
            if (x+ctn == 0 || x+ctn >= width) 
                break;

            SetPixel(y, x+ctn, message[ctn], color, backgroundColor);
        }
    }
    
    public void SetPixel(int y, int x, char symbol, 
                        ConsoleColor color = ConsoleColor.White, 
                        ConsoleColor backgroundColor = ConsoleColor.Black) {
        buffer[y,x] = new VirtualPixel(symbol, color, backgroundColor);
    }
    public void SetPixel(int y, int x, VirtualPixel pixel) {
        buffer[y,x] = pixel;
    }

    public void RestoreBuffer() {
        for(int y = 0; y < height; y++) {
            for(int x = 0; x < width; x++) {
                if (y == 0 || y == height-1) {
                    buffer[y,x] = new VirtualPixel(backgroundSymbol, backgroundSymbolColor, windowColor);
                } else if (x == 0 || x == width-1) {
                    buffer[y,x] = new VirtualPixel(backgroundSymbol, backgroundSymbolColor, windowColor);
                } else {
                    buffer[y,x] = new VirtualPixel(emptySymbol, backgroundSymbolColor, backgroundColor);
                }
            }
        }
    }

    public void Draw() {
        Console.Clear();
        Console.SetCursorPosition(0,0);

        if (rndMode == ERenderer.DEFAULT) {
            string symbols = "";
            for(int y = 0; y < height; y++) {
                for(int x = 0; x < width; x++) {
                    symbols = symbols + buffer[y, x].symbol;
                }
                
                symbols = symbols + "\n";
            }
            Console.Write(symbols);
        } else if (rndMode == ERenderer.COLORFUL) {
            for(int y = 0; y < height; y++) {
                for(int x = 0; x < width; x++) {
                    Console.BackgroundColor = buffer[y, x].backgroundColor;
                    Console.ForegroundColor = buffer[y, x].color;
                    Console.Write(buffer[y, x].symbol);
                }
                
                if (wndBackFillMode == EBackgroundFillMode.NONE)
                    Console.BackgroundColor = ConsoleColor.Black; 
                
                Console.Write("\n");
            }
        }
        /* COLORFUL DRAW

        So, it's very slowly method. Well, i disabled it

        for(int y = 0; y < height; y++) {
            for(int x = 0; x < width; x++) {
                Console.BackgroundColor = buffer[y, x].backgroundColor;
                Console.ForegroundColor = buffer[y, x].color;
                Console.Write(buffer[y, x].symbol);
            }
            
            if (wndBackFillMode == EBackgroundFillMode.NONE)
                Console.BackgroundColor = ConsoleColor.Black; 
            
            Console.Write("\n");
        }
        */
    }
}

struct VirtualPixel {
    public char symbol;
    public ConsoleColor color;
    public ConsoleColor backgroundColor;

    public VirtualPixel(
                    char _symbol, 
                    ConsoleColor _color = ConsoleColor.White, 
                    ConsoleColor _bColor = ConsoleColor.Black) 
    {
        symbol = _symbol;
        color = _color;
        backgroundColor = _bColor;
    }
}

// GUI ENGINE

class VirtualButton {
    public int x;
    public int y;
    public string text;

    public delegate void EventHandler();

    public event EventHandler onClick;

    public VirtualButton(int _y, int _x, string _text = "") {
        y = _y;
        x = _x;
        text = _text;
    }
    // Нормальное состояние кнопки. Когда она не фокусируется
    public void DrawNormal(VirtualScreen scrCtx) {
        scrCtx.SetText(y, x, text);
    }
    // Рисуется, когда она в фокусе под курсором из VirtualListView
    public void DrawFocusable(VirtualScreen scrCtx) {
        scrCtx.SetText(y, x, ">"+text, ConsoleColor.Black, ConsoleColor.Yellow);
    }
    // ...
    public void Click() {
        if (onClick == null) return;

        onClick();
    }
}

class VirtualListView {
    public List<VirtualButton> items = new List<VirtualButton>();
    public int x;
    public int y;
    public int padding;
    public bool visible;

    public VirtualButton? focused = null;

    public VirtualListView(int _y = 5, int _x = 5, int _padding = 1) {
        x = _x;
        y = _y;
        padding = _padding;
    }

    public void SetVisible(bool toggle) {
        visible = toggle;

        if (visible)
            Update();
    }

    public void Update() {
        if (items.Count < 1) return;

        focused = items[0];
    }

    public void Click() {
        if (focused == null) return;

        focused.Click();
    }

    public void Draw(VirtualScreen scrCtx) {
        if (!visible) return;

        var _y = 0;
        foreach (var item in items)
        {
            item.y = y + _y;
            item.x = x;
            if (focused == item)
                item.DrawFocusable(scrCtx);
            else
                item.DrawNormal(scrCtx);

            _y += 1 + padding;
        }
    }

    public void KeyPressed(ConsoleKey key) {
        if (!visible) return;
        if (focused == null) return;

        if (key == ConsoleKey.UpArrow)
            ScrollUp();
        else if (key == ConsoleKey.DownArrow)
            ScrollDown();
        else if (key == ConsoleKey.Enter)
            focused.Click();    
    }

    private void ScrollDown() {
        for(int i = 0; i < items.Count; i++) {
            if (items[i] == focused) {
                if (items.Count-1 < i+1)
                    focused = items[0];
                else
                    focused = items[i+1];

                break;
            }
        }
    }

    private void ScrollUp() {
        for(int i = 0; i < items.Count; i++) {
            if (items[i] == focused) {
                if (i-1 < 0)
                    focused = items[items.Count-1];
                else
                    focused = items[i-1];

                break;
            }
        }
    }
}

struct VirtualTexture {
    public VirtualPixel[,] buffer;

    public VirtualTexture(int _height, int _width) {
        buffer = new VirtualPixel[_height, _width];
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