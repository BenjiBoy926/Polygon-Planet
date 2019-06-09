using UnityEngine;

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
    /*
     * PUBLIC TYPEDEFS
     */
    [System.Serializable] public class FloatEvent : Event<float> { };

    // This floating-point value is sent to the activated event if the state is locked into the activated state
    public const float LOCKED_ACTIVATION_DURATION = -1f;

    [SerializeField]
    private string _label;   // Label to describe this state
    public string label { get { return _label; } }
    private bool mainState; // Current state

    private bool isLocked { get; set; }  // True if the state has been locked into true or false
    private bool lockedState;   // The state if it is being locked

    [SerializeField]
    [Tooltip("Set of events invoked when the state is activated")]
    private FloatEvent _stateActivatedEvent;
    public Event<float> stateActivatedEvent { get { return _stateActivatedEvent; } } 
    [SerializeField]
    [Tooltip("Set of events invoked when the state is deactivated")]
    private Event _stateDeactivatedEvent;
    public Event stateDeactivatedEvent { get { return _stateDeactivatedEvent; } }

    // State's implicitly converted to bool return true while active and false while inactive
    public static implicit operator bool(State state)
    {
        // If not locked, choose time comparison. If locked, choosed locked state
        return (state.mainState && !state.isLocked) || (state.lockedState && state.isLocked);
    }
    // Overload of the Activate () method allows calling method to specify a custom duration for the state
    public void Activate (float customDuration)
    {
        if(!isLocked)
        {
            // Set main state and schedule deactivation
            mainState = true;
            DeactivateAfterTime(customDuration);
            _stateActivatedEvent.Invoke(customDuration);
        } // endif not locked
    }
    // Disable the state by setting the timer to an invalid number
    public void Deactivate()
    {
        if(!isLocked)
        {
            // Set state to false
            mainState = false;
            _stateDeactivatedEvent.Invoke();
        } // endif not locked
    }
    // Lock the state to the bool specified
    // It cannot change states again until "Unlock" is called
    public void Lock (bool state)
    {
        // Set locked to true and locked state to the state specified
        isLocked = true;
        lockedState = state;

        // Reset timer and stop any invoked
        CancelInvoke();

        // Call state activated event if it was locked to true
        if(state)
        {
            _stateActivatedEvent.Invoke(LOCKED_ACTIVATION_DURATION);
        }
        // Call state deactivated event if it was locked to false
        else
        {
            _stateDeactivatedEvent.Invoke();
        }
    }
    public void Unlock()
    {
        isLocked = false;
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
