using App.Test.Components;
using App.Test.GameObjects;

namespace App.Test.Interfaces;

public interface IComponent {
    ComponentLifeStage LifeStage {get;}
    Entity Owner {get;}
}