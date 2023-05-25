using System;

namespace Engine;

public enum LifeStage: byte {
    Start,
    Initializing,
    Running,
    Exit
}

public interface IApplication {
    public void Initialize() {}
    public void Process() {}
    public void Dispose() {}
}

// Main app loop controller
public partial class Application : IApplication {

    public LifeStage LifeStage = LifeStage.Start;

    public Application(Point ScreenSize, Point FontSize, string? Font = null) {
        // Initialize screen
        Screen.Initialize(ScreenSize.X, ScreenSize.Y, FontSize.X, FontSize.Y, Font);
        // Initialize programm
        this.Initialize();
        // Main loop
        while (true) {
            // Application logic
            this.Process();
            // Rendering
            Screen.Draw();
            Screen.RestoreBuffer();
            // Check if we wanna exit from programm
            if (LifeStage == LifeStage.Exit) {
                break;
            }
        }
    }

    public virtual void Initialize() {}
    public virtual void Process() {}
    public virtual void Dispose() { LifeStage = LifeStage.Exit; }
}