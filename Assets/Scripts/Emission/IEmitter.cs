using UnityEngine;
using UnityEngine.Events;

[System.Serializable] public class EmissionEvent : UnityEvent<Vector2> { };

public interface IEmitter : IConsumer<Vector2>
{
    void Emit(Vector2 aim);
    EmissionEvent emissionEvent { get; }
}
