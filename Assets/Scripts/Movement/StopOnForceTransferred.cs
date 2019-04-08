using UnityEngine;
using System.Collections;

public class StopOnForceTransferred : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Force source that stops the mover when it transfers energy")]
    private ForceSource source;
    [SerializeField]
    [Tooltip("Mover that is stopped when the energy source transfers energy")]
    private KinematicMover2D mover;

    private void Start()
    {
        source.forceTransferredEvent += Stop;
    }

    private void Stop(ForceTransferredEventData eventData)
    {
        mover.Stop();
    }
}
