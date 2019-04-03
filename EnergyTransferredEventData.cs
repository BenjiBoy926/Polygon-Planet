using UnityEngine;

public class EnergyTransferredEventData
{
	private EnergySource _source;
	private EnergySocket _socket;
	
	public EnergySource source { get { return _source; } }
	public EnergySocket socket { get { return _socket; } }
	
	public EnergyTransferredEventData(EnergySource energySource, EnergySocket energySocket)
	{
		_source = energySource;
		_socket = energySocket;
	}
}
