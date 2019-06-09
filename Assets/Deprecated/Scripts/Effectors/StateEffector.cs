using UnityEngine;
using UnityEditor;

/*
 * CLASS StateEffector
 * -------------------
 * A type of effector that activates its effect when the state
 * of the specified object is activated and activates the
 * effect when it deactivates
 * -------------------
 */ 

public class StateEffector : EventEffector<State>
{
    [SerializeField]
    private bool inverted;  // If true, the particle DEactivates when the state activates and vice-versa

    protected override void Start()
    {
        base.Start();
        eventHandle.stateActivatedEvent.action += OnStateActivated;

        if(!inverted)
        {
            eventHandle.stateDeactivatedEvent.action += DisableEffect;
        }
        else
        {
            eventHandle.stateDeactivatedEvent.action += EnableEffect;
        }
    }
    // Added to the state activated event
    private void OnStateActivated(float duration)
    {
        if(!inverted)
        {
            EnableEffect();
        }
        else
        {
            DisableEffect();
        }
    }
}