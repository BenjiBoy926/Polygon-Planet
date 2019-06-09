using UnityEngine;
using System.Collections.Generic;

public class InputEvents : MonoBehaviour
{
    [SerializeField]
    [Tooltip("List of events that occur when the buttons are pressed")]
    private List<InputEvent> inputs;

    void Update()
    {
        foreach(InputEvent input in inputs)
        {
            input.CheckAndInvokeEvent();
        }
    }
}
