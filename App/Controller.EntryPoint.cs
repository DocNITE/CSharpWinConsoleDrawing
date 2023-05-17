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