using UnityEngine;
using System.Collections;

// Stop the given mover as soon as the given energy source transfers energy 
public class StopOnEnergyTransferred : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Energy source that stops the mover when it transfers energy")]
    protected EnergySource source;
    [SerializeField]
    [Tooltip("Mover that is stopped when the energy source transfers energy")]
    protected KinematicMover2D mover;

    protected virtual void Start()
    {
        source.energyTransferredEvent.AddListener(Stop);
    }

    protected virtual void Stop(EnergyTransferredEventData eventData)
    {
        mover.Stop();
    }
}
