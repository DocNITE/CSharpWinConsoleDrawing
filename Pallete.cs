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
			Screen.CONSOLE_SCREEN_BUFFER_INFO_EX csbe = new Screen.CONSOLE_SCREEN_BUFFER_INFO_EX();
			csbe.cbSize = Marshal.SizeOf(csbe);
			IntPtr hConsoleOutput = Screen.GetStdHandle(-11);
			if (hConsoleOutput == new IntPtr(-1)) {
				return Marshal.GetLastWin32Error();
			}
			bool brc = Screen.GetConsoleScreenBufferInfoEx(hConsoleOutput, ref csbe);
			if (!brc) {
				return Marshal.GetLastWin32Error();
			}

			switch (color) {
				case 0:
					csbe.black = new Screen.ColorRef(r, g, b);
					break;
				case 1:
					csbe.darkBlue = new Screen.ColorRef(r, g, b);
					break;
				case 2:
					csbe.darkGreen = new Screen.ColorRef(r, g, b);
					break;
				case 3:
					csbe.darkCyan = new Screen.ColorRef(r, g, b);
					break;
				case 4:
					csbe.darkRed = new Screen.ColorRef(r, g, b);
					break;
				case 5:
					csbe.darkMagenta = new Screen.ColorRef(r, g, b);
					break;
				case 6:
					csbe.darkYellow = new Screen.ColorRef(r, g, b);
					break;
				case 7:
					csbe.gray = new Screen.ColorRef(r, g, b);
					break;
				case 8:
					csbe.darkGray = new Screen.ColorRef(r, g, b);
					break;
				case 9:
					csbe.blue = new Screen.ColorRef(r, g, b);
					break;
				case 10:
					csbe.green = new Screen.ColorRef(r, g, b);
					break;
				case 11:
					csbe.cyan = new Screen.ColorRef(r, g, b);
					break;
				case 12:
					csbe.red = new Screen.ColorRef(r, g, b);
					break;
				case 13:
					csbe.magenta = new Screen.ColorRef(r, g, b);
					break;
				case 14:
					csbe.yellow = new Screen.ColorRef(r, g, b);
					break;
				case 15:
					csbe.white = new Screen.ColorRef(r, g, b);
					break;
			}

			++csbe.srWindow.Bottom;
			++csbe.srWindow.Right;

			brc = Screen.SetConsoleScreenBufferInfoEx(hConsoleOutput, ref csbe);
			if (!brc) {
				return Marshal.GetLastWin32Error();
			}
			return 0;
		}
}