internal interface ICloneable<T> {
    T Clone() {return (T)this;}
}

interface ICloneable {
    object Clone() {return this;}
}