// Initialize screen
Engine.Screen.Initialize(160, 40);
// Initialize game
App.Controller.Initialize();
// Main loop
while (true) {
    // Game logic
    App.Controller.Update();
    App.Controller.Render();
    // Rendering
    Engine.Screen.Draw();
    Engine.Screen.RestoreBuffer();
    // Input
    if (Console.KeyAvailable) {
        ConsoleKeyInfo input = Console.ReadKey();
        App.Controller.KeyHandle(input);
    }
    // Check if we wanna exit from game
    var exit = App.Controller.LifeStage == App.LifeStage.Exit;
    if (exit) {
        return 0;
    }
}