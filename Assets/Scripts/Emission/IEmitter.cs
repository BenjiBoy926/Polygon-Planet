using UnityEngine;

[System.Serializable]
public class EmissionEvent : OrderedUnityEventArg1 { };

public interface IEmitter : IConsumer<Vector2>
{
    void Emit(Vector2 aim);
    EmissionEvent emissionEvent { get; }
}
