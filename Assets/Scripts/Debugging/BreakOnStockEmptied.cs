using UnityEngine;
using System.Collections;

public class BreakOnStockEmptied : MonoBehaviour
{
    [SerializeField]
    private Stockpile stock;

    private void Start()
    {
        stock.stockEmptiedEvent += Break;
    }

    private void Break()
    {
        Debug.Log("Stock labelled " + stock.label + " is empty");
        //Debug.Break();
    }
}
