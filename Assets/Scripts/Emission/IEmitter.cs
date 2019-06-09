using UnityEngine;

public interface IEmitter
{
    void Emit(Vector2 aim);
    Event<Vector2> emissionEvent { get; }
}

[System.Serializable] public class EmissionEvent : Event<Vector2> { };