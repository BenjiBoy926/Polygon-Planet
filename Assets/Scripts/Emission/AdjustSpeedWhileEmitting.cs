using UnityEngine;
using System.Collections;

/*
 * CLASS AdjustSpeedWhileEmitting
 * ------------------------------
 * Change the speed of a kinematic mover controller
 * between two values - one while the given emitter
 * is emitting and one while it isn't
 * ------------------------------
 */ 

public class AdjustSpeedWhileEmitting : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Emitter that causes a speed adjustment on the given movement controller")]
    private ConstrainedEmitter2D emitter;
    [SerializeField]
    [Tooltip("Script that controls the speed of this object")]
    private KinematicMoverController controller;
    [SerializeField]
    [Tooltip("Speed of the object while the emitter is emitting")]
    private float emittingSpeed;
    [SerializeField]
    [Tooltip("Speed of the object while the emitter is not emitting")]
    private float idleSpeed;
    private void Start()
    {
        emitter.recentlyEmitted.stateActivatedEvent += SetEmittingSpeed;
        emitter.recentlyEmitted.stateDeactivatedEvent += SetIdleSpeed;
    }
    private void SetEmittingSpeed(float activeDuration)
    {
        controller.speed = emittingSpeed;
    }
    private void SetIdleSpeed()
    {
        controller.speed = idleSpeed;
    }
}
