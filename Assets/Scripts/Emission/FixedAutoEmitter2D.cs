using UnityEngine;
using System.Collections;

/*
 * CLASS FixedAutoEmitter2D
 * ------------------------
 * Causes an auto emitter to immediately start emitting at the start of a scene
 * fixed in the specified direction
 * ------------------------
 */ 

public class FixedAutoEmitter2D : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Script used to automatically emit the objects")]
    private AutoEmitter2D emitter;
    [SerializeField]
    [Tooltip("Fixed aiming vector of the auto emitter")]
    private Vector2 aimVector;
    private void Start()
    {
        emitter.StartAutoEmitting(AimVector);
    }
    private Vector2 AimVector()
    {
        return aimVector;
    }
}
