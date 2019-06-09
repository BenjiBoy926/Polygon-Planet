using UnityEngine;

// A type of script that only stops the kinematic mover if the given
// energy source actually transferred nonzero energy
public class StopIfNonzeroEnergyTransferred : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Energy source that stops the mover when it transfers energy")]
    protected EnergySource source;
    [SerializeField]
    [Tooltip("Mover that is stopped when the energy source transfers energy")]
    protected KinematicMover2D mover;

    private void Start()
    {
        source.energyTransferredEvent.action += Stop;
    }

    // Simulate the energy absorption on the energy source in the energy transfer event,
    // only call the stop method if the amount absorbed is non-zero
    private void Stop(EnergyTransferredEventData eventData)
    {
        int energyTransferred = eventData.socket.ProcessEnergy(eventData.source.energy);
        if(energyTransferred != 0)
        {
            mover.Stop();
        }
    }
}
