namespace Game.Test;

class Entity: ICloneable<Entity> {
    public Entity()
    {

    }
    public Entity Clone() {
        return new Entity();
    }
}

static class EntityManager {
    static List<Entity> _entityList = new();

    static public void AddEntity(Entity entity) {
        if (_entityList.HasElement(entity)) 
            return;

        _entityList.Add(entity);
    }
}