using UnityEngine;
using System.Collections;

// Log a description of a stock change on the given stockpile
public class ReportStockChange : MonoBehaviour
{
    [SerializeField]
    private Stockpile stockpile;
    // Use this for initialization
    void Start()
    {
        stockpile.stockChangedEvent += Report;
    }
    // Update is called once per frame
    private void Report(int delta)
    {
        Debug.Log("Stock labelled " + stockpile.label + " changed stock by " + delta + ". Current stock: " + stockpile.currentStock);
    }
}
