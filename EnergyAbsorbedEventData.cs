using UnityEngine;

public class EnergyAbsorbedEventData
{
	private EnergySocket _socket;
	private Energy _energy;
	private int _amountAbsorbed;
	
	public EnergySocket socket { get { return _socket; } }
	public Energy energy { get { return _energy; } }
	public int amountAbsorbed { get { return _amountAbsorbed; } }
	
	public EnergyAbsorbedEventData(EnergySocket energySocket, Energy energyPayload, int amountOfEnergyAbsorbed)
	{
		_socket = energySocket;
		_energy = energyPayload;
		_amountAbsorbed = amountOfEnergyAbsorbed
	}
}
