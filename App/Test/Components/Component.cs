using App.Test;
using App.Test.GameObjects;
using App.Test.Interfaces;

namespace App.Test.Components;

public class Component : IComponent {
    public ComponentLifeStage LifeStage { get; private set; } = ComponentLifeStage.Starting;

    public Entity? Owner { get; private set; }
}