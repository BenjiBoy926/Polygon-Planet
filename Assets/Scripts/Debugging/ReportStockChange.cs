using UnityEngine;
using System.Collections.Generic;

// Log a description of a stock change on the given stockpile
public class ReportStockChange : MonoBehaviour
{
    [SerializeField]
    private List<Stockpile> stockpiles;
    // Use this for initialization
    void Start()
    {
        foreach(Stockpile stock in stockpiles)
        {
            stock.stockChangedEvent.AddListener(Report);
        }
    }
    // Update is called once per frame
    private void Report(int delta)
    {
        //Debug.Log("Stock labelled " + stockpile.label + " changed stock by " + delta + ". Current stock: " + stockpile.currentStock);
    }
}
