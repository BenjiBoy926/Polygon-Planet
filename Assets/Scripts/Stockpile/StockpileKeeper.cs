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
    [Tooltip("If true, search the children of the game objects with the given tags " +
        "for the labelled stockpiles")]
    private bool includeChildren;
    [SerializeField]
    [Tooltip("The list of game object tags and the corresponding stockpile labels " +
        "that this script maintains a list of")]
    private List<LabelledComponentID> stockpileIDs;

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
    private LabelledComponentKeeper<Stockpile> stockpiles;

    private void Start()
    {
        stockpiles = new LabelledComponentKeeper<Stockpile>(stockpileIDs, includeChildren);

        foreach (Stockpile stock in stockpiles.components)
        {
            stock.stockEmptiedEvent.AddListener(CheckStocksEmptied);
            stock.stockFilledEvent.AddListener(CheckStocksFilled);
        }
    }

    private void CheckStocksEmptied()
    {
        if (stockpiles.components.TrueForAll(x => x.empty))
        {
            _allStocksEmptiedEvent.Invoke();
        }
    }

    private void CheckStocksFilled()
    {
        if (stockpiles.components.TrueForAll(x => x.full))
        {
            _allStocksFilledEvent.Invoke();
        }
    }
}
