using Engine;

namespace Game.Scenes;

class Scene {
    public Scene() {}

    public virtual void Initialize(VirtualScreen scrCtx) {}
    public virtual void Update(VirtualScreen scrCtx) {}
    public virtual void Render(VirtualScreen scrCtx) {}
    public virtual void KeyHandle(VirtualScreen scrCtx, ConsoleKeyInfo input) {}
}