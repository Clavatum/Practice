using UnityEngine;

public class TransformMemento
{
    public Vector3 Scale { get; }
    public Vector3 Position { get; }

    public TransformMemento(Vector3 scale, Vector3 position)
    {
        Scale = scale;
        Position = position;
    }
}