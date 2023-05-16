using System;
using Engine;
using App.Scenes;
using App.Resources;
using App.Scenes;

namespace App;

enum LifeStage: byte {
    Start,
    Initializing,
    Running,
    Exit
}

// Main game loop controller
static class Controller {

    static public LifeStage LifeStage = LifeStage.Start;
    static public Scene? Scene;
    
    static public void Initialize() {
        // test
        Scene = new SceneRenderingTest();
        Scene.Initialize(); // Why not?
    }
    // Main game process - physics, movement and some.
    static public void Update() {
        Scene?.Update();
    }
    // Main renderer method for drawing screen buffer
    static public void Render() {
        Scene?.Render();
    }
    // Input listeneer
    static public void KeyHandle(ConsoleKeyInfo input) {
        Scene?.KeyHandle(input);

        if (input.Key == ConsoleKey.Escape) {
            LifeStage = LifeStage.Exit;
        }
    }
}