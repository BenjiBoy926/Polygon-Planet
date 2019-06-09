using UnityEngine;
using System.Collections.Generic;

public class ReportEnergyAbsorbed : MonoBehaviour
{
    [SerializeField]
    private List<EnergySocket> energySockets;
    // Use this for initialization
    void Start()
    {
        foreach(EnergySocket socket in energySockets)
        {
            socket.energyAbsorbedEvent.action += Report;
        }
    }

    private void Report(EnergyAbsorbedEventData eventData)
    {
        Debug.Log("Socket on " + eventData.socket.gameObject.name + " absorbed " + eventData.amountAbsorbed);
    }
}
