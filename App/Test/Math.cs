using App.Test.Interfaces;

class Vector2: ICloneable<Vector2> {
    public float X {get; set;}
    public float Y {get; set;}
    public Vector2(float X, float Y) {
        this.X = X;
        this.Y = Y;
    }

    public Vector2 Clone()
    {
        return new Vector2(this.X, this.Y);
    }
}