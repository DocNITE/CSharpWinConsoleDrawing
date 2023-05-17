using System;
using Engine;

namespace App;

public enum LifeStage: byte {
    Start,
    Initializing,
    Running,
    Exit
}

public class IControllerMethods {
    public static void Initialize() {}
    public static void Update() {}
    public static void Render() {}
    public static void KeyHandle(ConsoleKeyInfo input) {}
    public static void Dispose() {}
}

// Main game loop controller
public partial class Controller : IControllerMethods {

    public static LifeStage LifeStage = LifeStage.Start;

    // So, when you import submodule in your project, next -
    // you need to implement second code into your app entry point file 
}

/* Controller.EntryPoint.cs

using System;
using Engine;
using App.Scenes;
using App.Resources;

namespace App;

public partial class Controller {
    public static Scene? Scene;
        
    public new static void Initialize() {
        // test
        Scene = new SceneRenderingTest();
        Scene.Initialize(); // Why not?
    }
     // Main game process - physics, movement and some.
    public new static void Update() {
        Scene?.Update();
    }
     // Main renderer method for drawing screen buffer
    public new static void Render() {
        Scene?.Render();
    }
     // Input listeneer
    public new static void KeyHandle(ConsoleKeyInfo input) {
        Scene?.KeyHandle(input);
        
        if (input.Key == ConsoleKey.Escape) {
            LifeStage = LifeStage.Exit;
        }
    }
}
*/