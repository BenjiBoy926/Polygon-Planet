using UnityEngine;
using System.Collections;

// Unlock the rotation of the given rotator
// when the given mover stops moving
public class UnlockRotationOnStop : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Reference to the script that has its rotation unlocked with the given emitter emits")]
    private LockableRotateToMouse2D rotator;
    [SerializeField]
    [Tooltip("Reference to the mover that causes the given rotator to unlock rotation")]
    private KinematicMover2D mover; 

    private void Start()
    {
        mover.stopEvent += Unlock;
    }

    private void Unlock()
    {
        rotator.UnlockRotation();
    }
}
