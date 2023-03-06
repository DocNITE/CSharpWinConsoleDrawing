using Engine;

namespace Game.Scenes;

class Scene {
    public Scene() {}

    public virtual void Initialize(VirtualScreen scrCtx) {}
    public virtual void PreProcess(VirtualScreen scrCtx) {}
    public virtual void Process(VirtualScreen scrCtx, ConsoleKeyInfo input) {}
}