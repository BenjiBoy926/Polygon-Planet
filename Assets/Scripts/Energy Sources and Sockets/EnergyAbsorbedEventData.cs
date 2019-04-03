using UnityEngine;

// Encapsulates information given when an energy socket absorbs energy
[System.Serializable]
public class EnergyAbsorbedEventData
{
    [SerializeField]
    private EnergySocket _socket;
    [SerializeField]
    private Energy _energy;
    [SerializeField]
    private int _amountAbsorbed;

    public EnergySocket socket { get { return _socket; } }
    public Energy energy { get { return _energy; } }
    public int amountAbsorbed { get { return _amountAbsorbed; } }

    public EnergyAbsorbedEventData(EnergySocket energySocket, Energy energyAbsorbed, int amtAbsorbed)
    {
        _socket = energySocket;
        _energy = energyAbsorbed;
        _amountAbsorbed = amtAbsorbed;
    }
}
