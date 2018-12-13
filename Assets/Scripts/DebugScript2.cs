using UnityEngine;
using System.Collections;

public class DebugScript2 : MonoBehaviour
{
    [SerializeField]
    private AutoEmitter2D emitter;

    private void Start()
    {
        emitter.AutoShoot(Right);
    }

    private Vector2 Right()
    {
        return Vector2.right;
    }
}
