using UnityEngine;
using System.Collections;


// Pause the game view as soon as the the given stockpile changes stock
public class BreakOnStockChanged : MonoBehaviour
{
    [SerializeField]
    private Stockpile stockpile;
    private void Start()
    {
        stockpile.stockChangedEvent += Break;
    }
    private void Break(int stockChange)
    {
        Debug.Break();
    }
}
