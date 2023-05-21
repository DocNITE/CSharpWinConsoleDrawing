using System.Runtime.InteropServices;

namespace Engine;

/// <summary>
///  https://github.com/ollelogdahl/ConsoleGameEngine
///  Thank you, you realy helped me!
/// </summary>
public class Pallete {
    public static int SetColor(int consoleColor, Color targetColor) {
			return SetColor(consoleColor, targetColor.R, targetColor.G, targetColor.B);
		}

		private static int SetColor(int color, uint r, uint g, uint b) {
			Kernel32.CONSOLE_SCREEN_BUFFER_INFO_EX csbe = new Kernel32.CONSOLE_SCREEN_BUFFER_INFO_EX();
			csbe.cbSize = Marshal.SizeOf(csbe);
			IntPtr hConsoleOutput = Kernel32.GetStdHandle(-11);
			if (hConsoleOutput == new IntPtr(-1)) {
				return Marshal.GetLastWin32Error();
			}
			bool brc = Kernel32.GetConsoleScreenBufferInfoEx(hConsoleOutput, ref csbe);
			if (!brc) {
				return Marshal.GetLastWin32Error();
			}

			switch (color) {
				case 0:
					csbe.black = new Kernel32.ColorRef(r, g, b);
					break;
				case 1:
					csbe.darkBlue = new Kernel32.ColorRef(r, g, b);
					break;
				case 2:
					csbe.darkGreen = new Kernel32.ColorRef(r, g, b);
					break;
				case 3:
					csbe.darkCyan = new Kernel32.ColorRef(r, g, b);
					break;
				case 4:
					csbe.darkRed = new Kernel32.ColorRef(r, g, b);
					break;
				case 5:
					csbe.darkMagenta = new Kernel32.ColorRef(r, g, b);
					break;
				case 6:
					csbe.darkYellow = new Kernel32.ColorRef(r, g, b);
					break;
				case 7:
					csbe.gray = new Kernel32.ColorRef(r, g, b);
					break;
				case 8:
					csbe.darkGray = new Kernel32.ColorRef(r, g, b);
					break;
				case 9:
					csbe.blue = new Kernel32.ColorRef(r, g, b);
					break;
				case 10:
					csbe.green = new Kernel32.ColorRef(r, g, b);
					break;
				case 11:
					csbe.cyan = new Kernel32.ColorRef(r, g, b);
					break;
				case 12:
					csbe.red = new Kernel32.ColorRef(r, g, b);
					break;
				case 13:
					csbe.magenta = new Kernel32.ColorRef(r, g, b);
					break;
				case 14:
					csbe.yellow = new Kernel32.ColorRef(r, g, b);
					break;
				case 15:
					csbe.white = new Kernel32.ColorRef(r, g, b);
					break;
			}

			++csbe.srWindow.Bottom;
			++csbe.srWindow.Right;

			brc = Kernel32.SetConsoleScreenBufferInfoEx(hConsoleOutput, ref csbe);
			if (!brc) {
				return Marshal.GetLastWin32Error();
			}
			return 0;
		}
}