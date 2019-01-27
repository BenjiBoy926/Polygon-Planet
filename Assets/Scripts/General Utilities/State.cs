using UnityEngine;
using UnityEngine.Events;

/*
 * CLASS State
 * -----------
 * Highly useful class makes keeping track of the state an object easy
 * States can be activated for a set amount of time or locked into the active/inactive
 * state. Robust event systems allow client code to have their methods called as soon
 * as the state is activated/deactivated
 * -----------
 */ 

public class State : MonoBehaviour
{
    public const float LOCKED_ACTIVATION_DURATION = -1f;    // This floating-point value is sent to the activated event if the state is locked into the activated state

    private float _duration; // Default duration the state lasts when activated
    private float timer;    // Timer used to determine if the state is active in time

    private bool _isLocked;  // True if the state has been locked into true or false
    private bool lockedState;   // The state if it is being locked

    // Event handling: class has an activated and deactivated event
    private UnityAction<float> onStateActivated;   // Multicast function pointer is called whenever the state activates
    private UnityAction onStateDeactivated; // Multicast funciton pointer is called as soon as the state is deactivated

    public float duration { get { return _duration; } }
    public bool isLocked { get { return _isLocked; } }

    public static State Construct(float duration = 1f, GameObject obj = null)
    {
        // If no object was specified for the state object, create one
        if(obj == null)
        {
            obj = new GameObject("State Object");
        }

        // Add a state component to the object specified and return it
        State state = obj.AddComponent<State>();
        state._duration = duration;
        return state;
    }

    // State's implicitly converted to bool return true while active and false while inactive
    public static implicit operator bool(State state)
    {
        // If not locked, choose time comparison. If locked, choosed locked state
        return (Time.time < state.timer && !state._isLocked) || (state.lockedState && state._isLocked);
    }

    // Activate the state by setting the timer to current time plus local duration
    public void Activate ()
    {
        if(!_isLocked)
        {
            timer = Time.time + duration;
            DeactivateAfterTime(duration);

            // Check before invoking the event
            if (onStateActivated != null)
            {
                onStateActivated(duration);
            } // endif not null
        } // endif not locked
    }
    // Overload of the Activate () method allows calling method to specify a custom duration for the state
    public void Activate (float customDuration)
    {
        if(!_isLocked)
        {
            timer = Time.time + customDuration;
            DeactivateAfterTime(customDuration);

            // Check before invoking the event
            if (onStateActivated != null)
            {
                onStateActivated(customDuration);
            } // endif not null
        } // endif not locked
    }
    // Disable the state by setting the timer to an invalid number
    public void Deactivate()
    {
        if(!_isLocked)
        {
            timer = -1f;

            // Check and invoke the event
            if (onStateDeactivated != null)
            {
                onStateDeactivated();
            } // endif not null
        } // endif not locked
    }

    // Lock the state to the bool specified
    // It cannot change states again until "Unlock" is called
    public void Lock (bool state)
    {
        // Set locked to true and locked state to the state specified
        _isLocked = true;
        lockedState = state;

        // Reset timer and stop any invokes
        timer = -1f;
        CancelInvoke();

        // Call state activated event if it was locked to true
        if(onStateActivated != null && state)
        {
            onStateActivated(LOCKED_ACTIVATION_DURATION);
        }
        // Call state deactivated event if it was locked to false
        else if(onStateDeactivated != null && !state)
        {
            onStateDeactivated();
        }
    }
    public void Unlock()
    {
        _isLocked = false;
    }

    // Add/remove the method specified to the activation event
    public void AddActivatedEvent(UnityAction<float> method)
    {
        onStateActivated += method;
    }
    public void RemoveActivatedEvent(UnityAction<float> method)
    {
        onStateActivated -= method;
    }
    // Add/remove the method specified to the deactivation event
    public void AddDeactivatedEvent(UnityAction method)
    {
        onStateDeactivated += method;
    }
    public void RemoveDeactivatedEvent(UnityAction method)
    {
        onStateDeactivated -= method;
    }

    // Schedule an invoke of the deactivate event
    private void DeactivateAfterTime(float timeout)
    {
        CancelInvoke();
        Invoke("Deactivate", timeout);
    }
}
