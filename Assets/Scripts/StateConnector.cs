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
    List<StateConnection> connections;

    private void Awake()
    {
        foreach(StateConnection connection in connections)
        {
            connection.EstablishConnection();
        }
    }
}
