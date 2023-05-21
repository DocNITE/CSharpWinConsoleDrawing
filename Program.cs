// Initialize screen
Engine.Screen.Initialize(300, 200);
// Initialize programm
App.Controller.Initialize();
// Main loop
while (true) {
    // Application logic
    App.Controller.Update();
    App.Controller.Render();
    // Rendering
    Engine.Screen.Draw();
    Engine.Screen.RestoreBuffer();
    // Input
    //if (Console.KeyAvailable) {
    //    ConsoleKeyInfo input = Console.ReadKey();
    //    App.Controller.KeyHandle(input);
    //}
    // Check if we wanna exit from programm
    var exit = App.Controller.LifeStage == App.LifeStage.Exit;
    if (exit) {
        return 0;
    }
}