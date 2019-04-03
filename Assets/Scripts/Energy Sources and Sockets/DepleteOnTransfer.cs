using UnityEngine;
using System.Collections;

/*
 * CLASS DepleteOnTransfer
 * -----------------------
 * When the given energy source transfers energy to a socket,
 * the given stockpile's stock decreases by 1
 * -----------------------
 */ 

public class DepleteOnTransfer : MonoBehaviour
{
    [SerializeField]
    private EnergySource source;
    [SerializeField]
    private Stockpile stock;
    private void Start()
    {
        source.energyTransferredEvent += DepleteStock;
    }
    private void DepleteStock(EnergyTransferredEventData data)
    {
        stock.ChangeStock(-1);
    }
}
