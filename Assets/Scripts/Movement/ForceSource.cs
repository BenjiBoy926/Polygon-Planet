using UnityEngine;
using System.Collections;

/*
 * CLASS ForceSource
 * -----------------
 * Transfers force to any force socket it comes into contact with
 * The direction of the force points away from the source towards the socket
 * -----------------
 */ 

public class ForceSource : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Strength of the blowback delivered to the blowback socket")]
    private float strength;
    [SerializeField]
    [Tooltip("Duration for which the force simulated mover is forced to moved")]
    private float time = ForceSimulatedMover2D.DEFAULT_BLOWBACK_TIME;

    // Event invoked when the source transfers force to a socket
    public event UnityAction<ForceTransferredEventData> forceTransferredEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ForceSocket socket = collision.GetComponent<ForceSocket>();
        // Cause the socket to absorb the force
        if(socket != null)
        {
            TransferForce(socket);
        }
    }

    private void TransferForce(ForceSocket socket)
    {
        // Use vector with tail at this and tip at the socket for the direction of the force
        Vector2 toSocket = socket.transform.position - transform.position;
        socket.AbsorbForce(toSocket, strength, time);
        // If event exists, invoke it
        if(forceTransferredEvent != null)
        {
            forceTransferredEvent(new ForceTransferredEventData(this, socket, toSocket, strength, time));
        }
    }
}
