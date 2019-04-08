using UnityEngine;
using System.Collections;

/*
 * CLASS PushbackStockChangeOnEnergyAbsorbed
 * -----------------------------------------
 * Each time the given energy socket absorbs energy,
 * the stock change scheduled on the ChangeStockOverTime
 * is pushed back for the given number of seconds
 * -----------------------------------------
 */

public class PushbackStockChangeOnEnergyAbsorbed : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Stock change is pushed back each time this socket absorbs energy")]
    private EnergySocket socket;
    [SerializeField]
    [Tooltip("This script controls the changes on the stockpile of interest")]
    private ChangeStockOverTime stockChanger;
    [SerializeField]
    [Tooltip("Stock change is pushed back for this amount of time each time the energy socket absorbs energy")]
    private float pushbackTime;

    void Start()
    {
        socket.energyAbsorbedEvent += PushbackStockChange;
    }
    private void PushbackStockChange(EnergyAbsorbedEventData eventData)
    {
        stockChanger.stockChangeNotReady.Activate(pushbackTime);
    }
}
