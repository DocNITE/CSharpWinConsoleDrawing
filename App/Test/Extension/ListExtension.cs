public static class ListExtension
{
    public static bool HasElement<T>(this List<T> list, T elem)
    {
        foreach (var item in list) {
            if ((object)item == (object)elem)
                return true;
        }
        return false;
    }
}