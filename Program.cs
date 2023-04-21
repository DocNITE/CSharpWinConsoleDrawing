// Initialize screen
Engine.Screen.Initialize(160, 40);
// Initialize game
Game.Controller.Initialize();
// Main loop
while (true) {
    // Game logic
    Game.Controller.Update();
    Game.Controller.Render();
    // Rendering
    Engine.Screen.Draw();
    Engine.Screen.RestoreBuffer();
    // Input
    if (Console.KeyAvailable) {
        ConsoleKeyInfo input = Console.ReadKey();
        Game.Controller.KeyHandle(input);
    }
    // Check if we wanna exit from game
    var exit = Game.Controller.LifeStage == Game.GameLifeStage.Exit;
    if (exit) {
        return 0;
    }
}