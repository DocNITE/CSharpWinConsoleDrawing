using System;
using Engine;
using App.Scenes;

namespace App;

public enum LifeStage: byte {
    Start,
    Initializing,
    Running,
    Exit
}

public class ControllerMethods {
    public static void Initialize() {}
    public static void Update() {}
    public static void Render() {}
    public static void KeyHandle(ConsoleKeyInfo input) {}
    public static void Dispose() {}
}

// Main game loop controller
public partial class Controller : ControllerMethods {

    public static LifeStage LifeStage = LifeStage.Start;

    // So, when you import submodule in your project, next -
    // you need to implement second code into your app entry point file 

    //public static Scene? Scene;
    //
    //public static void Initialize() {
    //    // test
    //    Scene = new Scene();
    //    Scene.Initialize(); // Why not?
    //}
    // // Main game process - physics, movement and some.
    //public static void Update() {
    //    Scene?.Update();
    //}
    // // Main renderer method for drawing screen buffer
    //public static void Render() {
    //    Scene?.Render();
    //}
    // // Input listeneer
    //public static void KeyHandle(ConsoleKeyInfo input) {
    //    Scene?.KeyHandle(input);
    //
    //    if (input.Key == ConsoleKey.Escape) {
    //        LifeStage = LifeStage.Exit;
    //    }
    //}
}

public class Controllerd: Controller {

}