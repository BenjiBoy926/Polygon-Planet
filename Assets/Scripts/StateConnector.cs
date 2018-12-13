using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * CLASS StateConnector
 * --------------------
 * The state connector is given pairs of scripts and attempts to 
 * "connect" the primary state of the sender to the primary state of the receiver by
 * making the receiver's state activate for the same amount of time
 * whenver the sender's state activates
 * --------------------
 */ 

public class StateConnector : MonoBehaviour
{
    [SerializeField]
    List<SenderRecieverPair> pairs;

    private void Awake()
    {
        ISingleStateObject sender;  // State on this object sends its signal to the receiver's primary state
        ISingleStateObject receiver;    // State on this object receives the signal from the sender's primary state
        bool successfulCast;    // True if the monobehaviours being connected were successfully cast as ISingleStateObject implementing

        foreach (SenderRecieverPair pair in pairs)
        {
            sender = pair.sender as ISingleStateObject;
            receiver = pair.receiver as ISingleStateObject;
            successfulCast = true;    // True if both monobehaviours successfully cast to single-state objects
            
            // Check to see if the monobehaviours implement the ISingStateObject interfaces
            if(sender == null)
            {
                Debug.LogError(pair.sender.gameObject.name + " does not implement the ISingleStateObject interface and cannot be connected by StateConnector on " + gameObject.name);
                successfulCast = false;
            }
            if(receiver == null)
            {
                Debug.LogError(pair.receiver.gameObject.name + " does not implement the ISingleStateObject interface and cannot be connected by StateConnector on " + gameObject.name);
                successfulCast = false;
            }

            // If the cast is successful, add the receiver's activate method to the senders activate event
            if(successfulCast)
            {
                sender.primaryState.AddActivatedEvent(receiver.primaryState.Activate);
                Debug.Log("State objects on " + pair.sender.gameObject.name + " and " + pair.receiver.gameObject.name + " successfully connected!");
            }
        }
    }
}
