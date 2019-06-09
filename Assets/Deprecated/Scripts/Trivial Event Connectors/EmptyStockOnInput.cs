using UnityEngine;
using System.Collections;

public class EmptyStockOnInput : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Stock to empty out when the input button is pressed")]
    private Stockpile stock;
    [SerializeField]
    [Tooltip("Name of the button in the input manager that empties the stockpile")]
    private string buttonName;
    [SerializeField]
    [Tooltip("Input is registered when the button is released (Up), pressed (Down), or every frame it is pressed (Stay)")]
    private InputButtonType buttonType;

    private void Update()
    {
        if(InputExt.GetButton(buttonName, buttonType))
        {
            stock.EmptyStock();
        }
    }
}
