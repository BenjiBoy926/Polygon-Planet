using UnityEngine;
using UnityEngine.Events;

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
     * PUBLIC TYPEDEFS
     */
    [System.Serializable] public class StockpileKeep : LabelledComponentKeeper<Stockpile> { };

    /*
     * PUBLIC DATA
     */
    [SerializeField]
    [Tooltip("The list of stockpiles that this class maintains")]
    private StockpileKeep _stockpiles;
    public StockpileKeep stockpiles { get { return _stockpiles; } }

    [SerializeField]
    [Tooltip("Invoked when all stockpiles in the list are filled up")]
    private UnityEvent _allStocksFilledEvent;
    public UnityEvent allStocksFilledEvent { get { return _allStocksFilledEvent; } }
    [SerializeField]
    [Tooltip("Invoked when all stockpiles in the list are emptied out")]
    private UnityEvent _allStocksEmptiedEvent;
    public UnityEvent allStocksEmptiedEvent { get { return _allStocksEmptiedEvent; } }

    private void Start()
    {
        _stockpiles = new StockpileKeep();

        foreach(Stockpile stock in _stockpiles.components)
        {
            stock.stockEmptiedEvent.AddListener(CheckStocksEmptied);
            stock.stockFilledEvent.AddListener(CheckStocksFilled);
        }
    }

    private void CheckStocksEmptied()
    {
        if (_stockpiles.components.TrueForAll(x => x.empty))
        {
            _allStocksEmptiedEvent.Invoke();
        }
    }

    private void CheckStocksFilled()
    {
        if (_stockpiles.components.TrueForAll(x => x.full))
        {
            _allStocksFilledEvent.Invoke();
        }
    }
}
