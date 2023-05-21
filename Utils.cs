namespace Engine;

public partial class Utility {
    public static bool HasMethod(object objectToCheck, string methodName)
    {
        var type = objectToCheck.GetType();
        return type.GetMethod(methodName) != null;
    } 

    public static int Clamp(int a, int min, int max) {
		a = (a > max) ? max : a;
		a = (a < min) ? min : a;

		return a;
	}

    // -screen:160,40,10,10
    public static List<string>? argv(string[] args, string idx) {
        foreach (var item in args)
        {
            var checkIndex = item.Split(':');
            if (checkIndex[0] == idx && checkIndex.Length > 1) {
                var values = checkIndex[1].Split(',');;
                var result = new List<string>();
                for (int i = 0; i < values.Length; i++) {
                    result.Add(values[i].ToString());
                }
                return result;
            }
        }
        return null;
    }
}