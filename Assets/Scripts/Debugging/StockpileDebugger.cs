using UnityEngine;
using System.Collections;

public class StockpileDebugger : MonoBehaviour
{
    /*
     * PUBLIC DATA
     */

    [SerializeField]
    [Tooltip("Stockpile to debug")]
    private Stockpile stockpile;
    [SerializeField]
    private DebugInfoSpecification stockChangedReport;
    [SerializeField]
    private DebugInfoSpecification stockFilledReport;
    [SerializeField]
    private DebugInfoSpecification stockEmptiedReport;

    /*
     * PRIVATE HELPERS
     */

    private void Start()
    {
        stockpile.stockChangedEvent.AddListener(DebugStockChange);
        stockpile.stockFilledEvent.AddListener(DebugStockFilled);
        stockpile.stockEmptiedEvent.AddListener(DebugStockEmpted);
    }
    private void DebugStockChange(int change)
    {
        DebugExt.DebugSpecifiedReport("Stock on stockpile labelled " + stockpile.label + " changed by " + change + ". Current stock: " + stockpile.currentStock, stockChangedReport);
    }
    private void DebugStockFilled()
    {
        DebugExt.DebugSpecifiedReport("Stock on stockpile labelled " + stockpile.label + " filled. Current stock: " + stockpile.currentStock, stockFilledReport);
    }
    private void DebugStockEmpted()
    {
        DebugExt.DebugSpecifiedReport("Stock on stockpile labelled " + stockpile.label + " emptied.", stockEmptiedReport);
    }
}
