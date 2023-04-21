using System;
using Engine;
using Game.Scenes;
using Game.Resources;

namespace Game;

enum EGameState: byte {
    START = 0,
    GAMEPLAY = 1,
    ENDING = 2,
    EXIT = 16
}

// Main game loop controller
static class Controller {

    static public EGameState State = EGameState.START;
    static public Scene? Scene;
    
    static public void Initialize() {
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
    static public bool KeyHandle(ConsoleKeyInfo input) {
        Scene?.KeyHandle(input);

        if (input.Key == ConsoleKey.Escape || State == EGameState.EXIT) {
            return false;
        }

        return true;
    }
}