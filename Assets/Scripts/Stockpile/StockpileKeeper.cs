using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

/*
 * CLASS StockpileKeeper
 * ---------------------
 * Keeps an internal list of the stockpiles in the scene
 * with the given labels, and publishes events that 
 * occur when all are filled up or emptied
 * ---------------------
 */ 

public class StockpileKeeper : MonoBehaviour
{
    /*
     * PUBLIC DATA
     */
    [SerializeField]
    [Tooltip("A reference to the script that can get the stockpile components")]
    private LabelledComponentKeeper stockpileFinder;

    [SerializeField]
    [Tooltip("Invoked when all stockpiles in the list are filled up")]
    private UnityEvent _allStocksFilledEvent;
    public UnityEvent allStocksFilledEvent { get { return _allStocksFilledEvent; } }
    [SerializeField]
    [Tooltip("Invoked when all stockpiles in the list are emptied out")]
    private UnityEvent _allStocksEmptiedEvent;
    public UnityEvent allStocksEmptiedEvent { get { return _allStocksEmptiedEvent; } }

    /*
     * PRIVATE DATA
     */
    private List<Stockpile> stockpiles;

    private void Start()
    {
        stockpiles = stockpileFinder.LabelledComponents<Stockpile>();

        foreach (Stockpile stock in stockpiles)
        {
            stock.stockEmptiedEvent.AddListener(CheckStocksEmptied);
            stock.stockFilledEvent.AddListener(CheckStocksFilled);
        }
    }

    private void CheckStocksEmptied()
    {
        if (stockpiles.TrueForAll(x => x.empty))
        {
            _allStocksEmptiedEvent.Invoke();
        }
    }

    private void CheckStocksFilled()
    {
        if (stockpiles.TrueForAll(x => x.full))
        {
            _allStocksFilledEvent.Invoke();
        }
    }
}
