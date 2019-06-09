using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InputEvent
{
    [SerializeField]
    [Tooltip("Name of the button in the input manager")]
    private string buttonName;
    [SerializeField]
    [Tooltip("Calls the event once the button is pressed, once the button is released, or each frame the button is held")]
    private InputButtonType buttonType;
    [SerializeField]
    [Tooltip("List of events invoked when the given button is pressed")]
    private UnityEvent callback;

    public void CheckAndInvokeEvent()
    {
        if(InputExt.GetButton(buttonName, buttonType))
        {
            callback.Invoke();
        }
    }
}
