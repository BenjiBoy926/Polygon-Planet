using UnityEngine;

public class ChangeStockWhileButtonHeld : MonoBehaviour
{
    /*
     * PUBLIC DATA
     */

    [SerializeField]
    [Tooltip("Script in charge of changing the stock")]
    private ChangeStockOverTime stockChanger;
    [SerializeField]
    [Tooltip("Name of the button in the input manager that causes the stock change")]
    private string buttonName;

    /*
     * PRIVATE HELPERS
     */

    private void Update()
    {
        // Start changing once button is clicked
        if(Input.GetButtonDown(buttonName))
        {
            stockChanger.StartStockChange();
        }
        // Stop changing once button released
        if(Input.GetButtonUp(buttonName))
        {
            stockChanger.StopStockChange();
        }
    }
}
