using System;
using Engine;
using Game.Scenes;
using Game.Resources;

namespace Game;

enum EGameState {
    INTRO = 0,
    GAMEPLAY = 1,
    ENDING = 2,
    EXIT = 16
}

// Main game loop handler
static class Manager {

    //static VirtualListView listMenu = new VirtualListView(4, 4, 1);
    static public EGameState gameState = EGameState.INTRO;
    static public Scene gameScene = new SceneSnake();
    
    static public void Initialize(VirtualScreen scrCtx) {
        /*
            listMenu.items.Add(new VirtualButton(0,0,"Button 1"));
        listMenu.items.Add(new VirtualButton(0,0,"Button 2"));
        listMenu.items.Add(new VirtualButton(0,0,"Button 3"));
        var unique_button = new VirtualButton(0,0,"Button 3.5");
        unique_button.onClick += TestMethod;
        listMenu.items.Add(unique_button);
        listMenu.items.Add(new VirtualButton(0,0,"Button 4"));
        listMenu.Update();
        */
    }
    // Используется в основном для рендеринга картинки.
    static public void Update(VirtualScreen scrCtx) {
        //listMenu.Draw(scrCtx);
        gameScene.Update(scrCtx);
    }
    static public void Render(VirtualScreen scrCtx) {
        gameScene.Render(scrCtx);
    }
    // Используется во время нажатия на кнопки
    static public bool KeyHandle(VirtualScreen scrCtx, ConsoleKeyInfo input) {
        //listMenu.KeyPressed(input.Key);
        gameScene.KeyHandle(scrCtx, input);

        if (input.Key == ConsoleKey.Escape || gameState == EGameState.EXIT) {
            return false;
        }

        return true;
    }
}