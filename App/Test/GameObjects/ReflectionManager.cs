using System.Reflection;

namespace App.Test.GameObjects;

public class ReflectionManager {
    public static IEnumerable<Type> GetTypesWithRegisterComp(Assembly assembly) {
        foreach(Type type in assembly.GetTypes()) {
            if (type.GetCustomAttributes(typeof(RegisterComponentAttribute), true).Length > 0) {
                yield return type;
            }
        }
    }

    public static object CreateInstance(Type type)
    {
        if (type == null)
            throw new ArgumentNullException(nameof(type));

        var instance = Activator.CreateInstance(type)!;

        //if (inject)
        //    _dependencies.InjectDependencies(instance, oneOff);
        return instance;
    }
}