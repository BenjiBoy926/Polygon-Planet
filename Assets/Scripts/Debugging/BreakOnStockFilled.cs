using UnityEngine;

public class BreakOnStockFilled : MonoBehaviour
{
    [SerializeField]
    private Stockpile stock;

    private void Start()
    {
        stock.stockFilledEvent.action += Break;
    }

    private void Break()
    {
        Debug.Log("Stockpile labelled " + stock.label + " is full.  Stock level: " + stock.currentStock);
        //Debug.Break();
    }
}
