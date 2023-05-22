using Microsoft.Win32.SafeHandles;
using static Engine.Kernel32;

namespace Engine;

public class Buffer {
    int width;
    int height;
    CharInfo[] buffer;
    public SafeFileHandle sf_handler;
    public Buffer(int w, int h) {
        width = w;
        height = h;
        buffer = new CharInfo[w * h];
        sf_handler = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);
    }
    /// <summary>
    /// Migrate screen buffer into char buffer for console drawing.
    /// </summary>
    /// <param name="Buffer"> screen buffer </param>
    public void SetBuffer(Pixel[] Buffer) {
        for (int i = 0; i < buffer.Length; ++i)
        {
            buffer[i].Attributes = (short)(Buffer[i].Color | (Buffer[i].BackgroundColor));
            buffer[i].Char.UnicodeChar = Buffer[i].Char;
        }
    } 
    /// <summary>
    /// Update console screen
    /// </summary>
    public void Blit() {
        if (!sf_handler.IsInvalid)
        {
            Rect rect = new Rect() { Left = 0, Top = 0, Right = (short)width, Bottom = (short)height };
            bool b = WriteConsoleOutputW(sf_handler, buffer,
              new Coord() { X = (short)width, Y = (short)height },
              new Coord() { X = 0, Y = 0 },
              ref rect);
        }
    }
}