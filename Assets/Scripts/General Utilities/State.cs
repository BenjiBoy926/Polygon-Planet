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

public class State : MonoBehaviour, ILabelledComponent
{
    // This floating-point value is sent to the activated event if the state is locked into the activated state
    public const float LOCKED_ACTIVATION_DURATION = -1f;

    [SerializeField]
    private string _label;   // Label to describe this state
    public string label { get { return _label; } }
    private bool mainState; // Current state

    private bool _isLocked;  // True if the state has been locked into true or false
    private bool lockedState;   // The state if it is being locked

    // Event handling
    // Event called whenever the state Activate() method is called
    public event UnityAction<float> stateActivatedEvent;
    // Event called whenever the state Deactivate() method is called
    public event UnityAction stateDeactivatedEvent;

    public bool isLocked { get { return _isLocked; } }

    public static State Construct(string theLabel = "DefaultState", GameObject obj = null)
    {
        // If no object was specified for the state object, create one
        if(obj == null)
        {
            obj = new GameObject("State Object");
        }

        // Add a state component to the object specified and return it
        State state = obj.AddComponent<State>();
        state._label = theLabel;
        return state;
    }
    // State's implicitly converted to bool return true while active and false while inactive
    public static implicit operator bool(State state)
    {
        // If not locked, choose time comparison. If locked, choosed locked state
        return (state.mainState && !state._isLocked) || (state.lockedState && state._isLocked);
    }
    // Overload of the Activate () method allows calling method to specify a custom duration for the state
    public void Activate (float customDuration)
    {
        if(!_isLocked)
        {
            // Set main state and schedule deactivation
            mainState = true;
            DeactivateAfterTime(customDuration);

            // Check before invoking the event
            if (stateActivatedEvent != null)
            {
                stateActivatedEvent(customDuration);
            } // endif not null
        } // endif not locked
    }
    // Disable the state by setting the timer to an invalid number
    public void Deactivate()
    {
        if(!_isLocked)
        {
            // Set state to false
            mainState = false;

            // Check and invoke the event
            if (stateDeactivatedEvent != null)
            {
                stateDeactivatedEvent();
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

        // Reset timer and stop any invoked
        CancelInvoke();

        // Call state activated event if it was locked to true
        if(stateActivatedEvent != null && state)
        {
            stateActivatedEvent(LOCKED_ACTIVATION_DURATION);
        }
        // Call state deactivated event if it was locked to false
        else if(stateDeactivatedEvent != null && state)
        {
            stateDeactivatedEvent();
        }
    }
    public void Unlock()
    {
        _isLocked = false;
    }
    // Schedule an invoke of the deactivate event
    private void DeactivateAfterTime(float timeout)
    {
        CancelInvoke();
        Invoke("Deactivate", timeout);
    }

    // Find all game objects with the given tag, then try to find a single stockpile on each game object with the tag given
    public static State[] FindStatesWithLabel(string gObjectTag, string stockpileLabel)
    {
        return ComponentUtility.FindComponentsWithLabel<State>(gObjectTag, stockpileLabel);
    }
    // Try to find a stockpile on each of the game objects given
    public static State[] FindStatesWithLabel(GameObject[] gObjects, string stockpileLabel)
    {
        return ComponentUtility.FindComponentsWithLabel<State>(gObjects, stockpileLabel);
    }
    // Find a game object with the given tag, then find a stockpile on that game object with the given tag
    public static State FindStateWithLabel(string gObjectTag, string stockpileLabel)
    {
        return ComponentUtility.FindComponentWithLabel<State>(gObjectTag, stockpileLabel);
    }
    // Find a stockpile on the given game object with the given tag
    public static State FindStateWithLabel(GameObject gObject, string stockpileLabel)
    {
        return ComponentUtility.FindComponentWithLabel<State>(gObject, stockpileLabel);
    }
}
