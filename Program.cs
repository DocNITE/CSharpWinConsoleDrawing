using static Engine.Utility;
// Initialize fonts
var _font = "";
if (argv(args, "font") != null) 
    _font = argv(args, "font")[0];
else 
    _font = null;
// Initialize screen
var _res = argv(args, "screen");
if (_res == null)
    Engine.Screen.Initialize(160, 40, 14, 18, _font);
else 
    Engine.Screen.Initialize(int.Parse(_res[0]),    // window width
                            int.Parse(_res[1]),     // window height
                            int.Parse(_res[2]),     // font width
                            int.Parse(_res[3]),     // font height
                            _font);    
// Initialize programm
App.Controller.Initialize();
// Main loop
while (true) {
    // Application logic
    App.Controller.Process();
    // Rendering
    Engine.Screen.Draw();
    Engine.Screen.RestoreBuffer();
    // Check if we wanna exit from programm
    if (App.Controller.LifeStage == App.LifeStage.Exit) {
        return 0;
    }
}