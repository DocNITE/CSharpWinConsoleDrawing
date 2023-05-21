using System.Drawing;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Engine;

/// <summary>
///  Some methods for console window from Kernel32.dll
///  Some code parts i grab there:
///  https://github.com/ollelogdahl/ConsoleGameEngine
///  Thank you, you realy helped me!
/// </summary>

public partial class Kernel32 {
    public const int MF_BYCOMMAND = 0x00000000;
    public const int SC_CLOSE = 0xF060;
    public const int SC_MINIMIZE = 0xF020;
    public const int SC_MAXIMIZE = 0xF030;
    public const int SC_SIZE = 0xF000;

    [DllImport("user32.dll")]
    public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

    [DllImport("user32.dll")]
    public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

    [DllImport("kernel32.dll", ExactSpelling = true)]
    public static extern IntPtr GetConsoleWindow();
    [DllImport("user32.dll", SetLastError = true)]
	public static extern short GetAsyncKeyState(Int32 vKey);
	[DllImport("user32.dll", SetLastError = true)]
	public static extern bool GetCursorPos(out Point vKey);
	[DllImport("user32.dll", SetLastError = true)]
	public static extern IntPtr GetForegroundWindow();


	[DllImport("user32.dll", SetLastError = true)]
	public static extern bool GetWindowRect(IntPtr hWnd, ref Rect lpRect);
	[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
	public static extern IntPtr GetDesktopWindow();

	[DllImport("user32.dll", SetLastError = true)]
	public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
	[DllImport("user32.dll", SetLastError = true)]
	public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
	[DllImport("user32.dll", SetLastError = true)]
	public static extern bool DrawMenuBar(IntPtr hWnd);
	[DllImport("user32.dll", SetLastError = true)]
	public static extern int MapWindowPoints(IntPtr hWndFrom, IntPtr hWndTo, [In, Out] ref Rect rect, [MarshalAs(UnmanagedType.U4)] int cPoints);



	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern IntPtr GetStdHandle(int nStdHandle);

    [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    public static extern SafeFileHandle CreateFile(
        string fileName,
        [MarshalAs(UnmanagedType.U4)] uint fileAccess,
        [MarshalAs(UnmanagedType.U4)] uint fileShare,
        IntPtr securityAttributes,
        [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
        [MarshalAs(UnmanagedType.U4)] int flags,
        IntPtr template);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool WriteConsoleOutputW(
      SafeFileHandle hConsoleOutput,
      CharInfo[] lpBuffer,
      Coord dwBufferSize,
      Coord dwBufferCoord,
      ref Rect lpWriteRegion);

    [DllImport("kernel32.dll", SetLastError = true)]
	public static extern bool GetConsoleScreenBufferInfoEx( IntPtr hConsoleOutput, ref CONSOLE_SCREEN_BUFFER_INFO_EX csbe );

	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern bool SetConsoleScreenBufferInfoEx( IntPtr ConsoleOutput, ref CONSOLE_SCREEN_BUFFER_INFO_EX csbe );

	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern Int32 SetCurrentConsoleFontEx(
	IntPtr ConsoleOutput,
	bool MaximumWindow,
	ref CONSOLE_FONT_INFO_EX ConsoleCurrentFontEx);

	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

    [StructLayout(LayoutKind.Sequential)]
    public struct Coord
    {
        public short X;
        public short Y;

        public Coord(short X, short Y)
        {
            this.X = X;
            this.Y = Y;
        }
    };

    [StructLayout(LayoutKind.Explicit)]
    public struct CharUnion
    {
        [FieldOffset(0)] public ushort UnicodeChar;
        [FieldOffset(0)] public byte AsciiChar;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct CharInfo
    {
        [FieldOffset(0)] public CharUnion Char;
        [FieldOffset(2)] public short Attributes;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Rect
    {
        public short Left;
        public short Top;
        public short Right;
        public short Bottom;
    }

    [StructLayout(LayoutKind.Sequential)]
	public struct SmallRect {
		public short Left;
		public short Top;
		public short Right;
		public short Bottom;
	}

    [StructLayout(LayoutKind.Sequential)]
	public struct ColorRef {
		internal uint ColorDWORD;

		internal ColorRef(Color color) {
			ColorDWORD = (uint)color.R + (((uint)color.G) << 8) + (((uint)color.B) << 16);
		}

		internal ColorRef(uint r, uint g, uint b) {
			ColorDWORD = r + (g << 8) + (b << 16);
		}

		internal Color GetColor() {
			return new Color((int)(0x000000FFU & ColorDWORD),
			   (int)(0x0000FF00U & ColorDWORD) >> 8, (int)(0x00FF0000U & ColorDWORD) >> 16);
		}

		internal void SetColor(Color color) {
			ColorDWORD = (uint)color.R + (((uint)color.G) << 8) + (((uint)color.B) << 16);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct CONSOLE_SCREEN_BUFFER_INFO_EX {
		public int cbSize;
		public Coord dwSize;
		public Coord dwCursorPosition;
		public short wAttributes;
		public SmallRect srWindow;
		public Coord dwMaximumWindowSize;

		public ushort wPopupAttributes;
		public bool bFullscreenSupported;

		internal ColorRef black;
		internal ColorRef darkBlue;
		internal ColorRef darkGreen;
		internal ColorRef darkCyan;
		internal ColorRef darkRed;
		internal ColorRef darkMagenta;
		internal ColorRef darkYellow;
		internal ColorRef gray;
		internal ColorRef darkGray;
		internal ColorRef blue;
		internal ColorRef green;
		internal ColorRef cyan;
		internal ColorRef red;
		internal ColorRef magenta;
		internal ColorRef yellow;
		internal ColorRef white;
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct CONSOLE_FONT_INFO_EX {
		public uint cbSize;
		public uint nFont;
		public Coord dwFontSize;
		public int FontFamily;
		public int FontWeight;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] // Edit sizeconst if the font name is too big
		public string FaceName;
	}
}