using UnityEngine;

public interface IEmitter
{
    void Emit(Vector2 aim);
    event UnityAction<Vector2> emissionEvent;
}
