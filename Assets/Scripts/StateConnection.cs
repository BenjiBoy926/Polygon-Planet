using UnityEngine;
using UnityEditor;

/*
 * CLASS StateConnection
 * ---------------------
 * Class takes two monobehaviours, attempts to interpret them as single state objects,
 * and connect them so that when the primary state activates on one object, it activates on the other
 * ---------------------
 */ 

[System.Serializable]
public class StateConnection
{
    [SerializeField]
    private SenderRecieverPair pair;
    [SerializeField]
    private bool useSenderDuration; // If true, the receiver's state is set for the same amount of time as the sender's state.  If false, the receiver activates for its own local duration
    private bool connected = false; // True once the connection has been established

    // Establish the connection
    public void EstablishConnection()
    {
        if(!connected)
        {
            ISingleStateObject sender;  // State on this object sends its signal to the receiver's primary state
            ISingleStateObject receiver;    // State on this object receives the signal from the sender's primary state
            bool successfulCast;    // True if the monobehaviours being connected were successfully cast as ISingleStateObject implementing

            sender = pair.sender as ISingleStateObject;
            receiver = pair.receiver as ISingleStateObject;
            successfulCast = true;    // True if both monobehaviours successfully cast to single-state objects

            // Check to see if the monobehaviours implement the ISingStateObject interfaces
            if (sender == null)
            {
                Debug.LogError(pair.sender.gameObject.name + " does not implement the ISingleStateObject interface and cannot be connected");
                successfulCast = false;
            }
            if (receiver == null)
            {
                Debug.LogError(pair.receiver.gameObject.name + " does not implement the ISingleStateObject interface and cannot be connected");
                successfulCast = false;
            }

            // If the cast is successful, add the receiver's activate method to the senders activate event
            if (successfulCast)
            {
                if(useSenderDuration)
                {
                    sender.primaryState.AddActivatedEvent(receiver.primaryState.Activate);
                }
                else
                {
                    sender.primaryState.AddActivatedEvent(receiver.primaryState.ActivateDummy);
                }
                Debug.Log("State objects on " + pair.sender.gameObject.name + " and " + pair.receiver.gameObject.name + " successfully connected!");
            }

            connected = true;
        }
        else
        {
            Debug.Log("Scripts on " + pair.sender.gameObject.name + " and " + pair.receiver.gameObject.name + " have alread been connected");
        }
    }
}