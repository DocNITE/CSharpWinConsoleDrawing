using Engine;
using Game.Test;

namespace Game.Scenes;

/// <summary>
/// debug test scene for some engine's shit
/// </summary>
class SceneRenderingTest: Scene {
    public SceneRenderingTest() {}

    public override void Initialize() {
        Screen.Title = "Rendering Game Test";
    }
    public override void Update() {}
    public override void Render() {
        Screen.SetText(1, 1, "Some white");
    }
    public override void KeyHandle(ConsoleKeyInfo input) {}
    public override void Dispose() {}
}