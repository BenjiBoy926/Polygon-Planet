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

public class DelayStockChangeOnEnergyAbsorbed : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Stock change is delayed each time this socket absorbs energy")]
    private EnergySocket socket;
    [SerializeField]
    [Tooltip("This script controls the changes on the stockpile of interest")]
    private ChangeStockOverTime stockChanger;
    [SerializeField]
    [Tooltip("Stock change is delayed for this amount of time each time the energy socket absorbs energy")]
    private float delayTime;

    void Start()
    {
        socket.energyAbsorbedEvent += DelayStockChange;
    }
    private void DelayStockChange(EnergyAbsorbedEventData eventData)
    {
        stockChanger.DelayStockChange(delayTime);
    }
}
