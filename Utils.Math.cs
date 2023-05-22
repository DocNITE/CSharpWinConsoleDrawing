namespace Engine;

public partial class Utility {

    public static int Clamp(int a, int min, int max) {
		a = (a > max) ? max : a;
		a = (a < min) ? min : a;

		return a;
	}
}