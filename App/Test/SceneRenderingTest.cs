using Engine;
using App.Test;
using App.Test.Components;
using App.Test.GameObjects;

namespace App.Scenes;

/// <summary>
/// debug test scene for some engine's shit
/// </summary>
public class SceneRenderingTest: Scene {
    public SceneRenderingTest() {}
    IEnumerable<Type> atype;

    public override void Initialize() {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        if (assemblies != null)
        {
            foreach (var asm in assemblies)
            {
                var atypes = ReflectionManager.GetTypesWithRegisterComp(asm);

                if (atypes == null)
                    continue;

                if (atypes.ToArray<Type>().Length <= 0)
                    continue;

                foreach (var item in atypes)
                {
                    TransformComponent classCustom = (TransformComponent)ReflectionManager.CreateInstance(item);
                    classCustom.beep();
                }
                atype = atypes;
                break;
            }
        }
        /*
            Осталось добавить систему ивентов (EventBus), прикручивать к ним функции из компонент системы,
            И можно делатьи гру
        */
    }
    public override void Update() {}
    public override void Render() {
        Screen.SetText(1, 1, atype.ToArray<Type>()[0].Name);
    }
    public override void KeyHandle(ConsoleKeyInfo input) {}
    public override void Dispose() {}
}

/*
/// <summary>
/// Component system use TEST
/// </summary>
class SceneRenderingTest: Scene {
    public SceneRenderingTest() {}
    IEnumerable<Type> atype;

    public override void Initialize() {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        if (assemblies != null)
        {
            foreach (var asm in assemblies)
            {
                var atypes = ReflectionManager.GetTypesWithRegisterComp(asm);

                if (atypes == null)
                    continue;

                if (atypes.ToArray<Type>().Length <= 0)
                    continue;

                foreach (var item in atypes)
                {
                    TransformComponent classCustom = (TransformComponent)ReflectionManager.CreateInstance(item);
                    classCustom.beep();
                }
                atype = atypes;
                break;
            }
        }
    }
    public override void Update() {}
    public override void Render() {
        Screen.SetText(1, 1, atype.ToArray<Type>()[0].Name);
    }
    public override void KeyHandle(ConsoleKeyInfo input) {}
    public override void Dispose() {}
}
*/