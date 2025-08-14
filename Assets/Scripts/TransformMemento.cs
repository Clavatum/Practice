using UnityEngine;

public class TransformMemento
{
    public Vector3 Scale { get; }
    public Vector3 Position { get; }
    public CapsuleCollider CapsuleCollider { get; }

    public TransformMemento(Vector3 scale, Vector3 position, CapsuleCollider capsuleCollider)
    {
        Scale = scale;
        CapsuleCollider = capsuleCollider;
        Position = position;
    }
}