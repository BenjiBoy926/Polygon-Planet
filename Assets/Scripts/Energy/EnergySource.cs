using UnityEngine;
using UnityEngine.Events;

/*
 * CLASS EnergySource
 * ------------------
 * Generic source of energy transfers energy to any energy socket it
 * comes into physical contact with. Energy sockets interpret the energy
 * and raises an event, passing amount actually absorbed
 * ------------------
 */ 

public class EnergySource : MonoBehaviour
{
    /*
     * PUBLIC TYPEDEFS
     */
    [System.Serializable] public class EnergyTransferredEvent : UnityEvent<EnergyTransferredEventData> { };

    [SerializeField]
    private Energy _energy;
    public Energy energy { get { return _energy; } }
    [SerializeField]
    [Tooltip("Set of events invoked when the source transfers its energy to a socket")]
    private EnergyTransferredEvent _energyTransferredEvent;
    public EnergyTransferredEvent energyTransferredEvent { get { return _energyTransferredEvent; } }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If an energy socket is found on the other object, transfer energy to it
        EnergySocket socket = collision.GetComponentInChildren<EnergySocket>();
        if(socket != null)
        {
            TransferEnergy(socket);
        }
    }
    // Transfer energy to the given energy socket and call the event if it exists
    protected virtual void TransferEnergy(EnergySocket socket)
    {
        int amountAbsorbed = socket.AbsorbEnergy(_energy);
        if(energyTransferredEvent != null)
        {
            energyTransferredEvent.Invoke(new EnergyTransferredEventData(socket, this, amountAbsorbed));
        }
    }
    // Scale up or scale down the current energy level of the energy source
    public void ScalePowerLevel(float multiplier)
    {
        int newPower = Mathf.RoundToInt(_energy.power * multiplier);
        _energy = new Energy(newPower, _energy.type, _energy.tag);
    }
}
