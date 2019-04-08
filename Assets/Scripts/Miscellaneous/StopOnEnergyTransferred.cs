using UnityEngine;
using System.Collections;

// Stop the given mover as soon as the given energy source transfers energy 
public class StopOnEnergyTransferred : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Energy source that stops the mover when it transfers energy")]
    private EnergySource source;
    [SerializeField]
    [Tooltip("Mover that is stopped when the energy source transfers energy")]
    private KinematicMover2D mover;

    private void Start()
    {
        source.energyTransferredEvent += Stop;
    }

    private void Stop(EnergyTransferredEventData eventData)
    {
        mover.Stop();
    }
}
