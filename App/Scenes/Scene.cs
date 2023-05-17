namespace App.Scenes;

public class Scene {
    public Scene() {}

    public virtual void Initialize() {}
    public virtual void Update() {}
    public virtual void Render() {}
    public virtual void KeyHandle(ConsoleKeyInfo input) {}
    public virtual void Dispose() {}
}