using UnityEngine;
using System.Collections;

// Lock the rotation of the rotate to mouse script 
// when the given emitter emits
public class LockRotationOnEmission : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Rotator to lock when the given emitter emits")]
    private LockableRotateToMouse2D rotator;
    [SerializeField]
    [Tooltip("Emitter that causes the rotation lock when it emits")]
    private Emitter2D emitter;

    private void Start()
    {
        //emitter.emissionEvent.AddListener(Lock);
    }

    private void Lock(Vector2 emissionDir)
    {
        rotator.LockRotation();
    }
}
