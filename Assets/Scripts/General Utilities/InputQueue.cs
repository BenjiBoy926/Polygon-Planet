using UnityEngine;
using System.Collections.Generic;

/*
 * CLASS InputQueue
 * ----------------
 * Receives input from the UnityEngine's "Input" static class
 * and stores the name of the input button in a queue if pressed
 * 
 * While it'd be possible to queue inputs that don't result in a bool
 * (such as Input.GetAxis), there is no way of retrieving the value 
 * generated when the button was clicked. Thus the input queue only
 * provides queuing for input functions that return bool
 * ----------------
 */ 

public static class InputQueue
{
    private static Queue<string> inputButtons = new Queue<string>(); // Queue of input buttons
    public static int maxBuffer = 1;  // The max number of inputs allowed in the queue

    // True if there is input waiting in the queue
    public static bool inputIsQueued
    {
        get
        {
            return inputButtons.Count > 0;
        }
    }

    // Use input extension to check the given button
    // Enqueue the button name if it was pressed
    public static void EnqueueInput(string buttonName, InputButtonType type)
    {
        if(InputExt.GetButton(buttonName, type) && inputButtons.Count < maxBuffer)
        {
            inputButtons.Enqueue(buttonName);
        }
    }

    // Dequeue the input
    public static string DequeueInput()
    {
        return inputButtons.Dequeue();
    }

    // Peek the next input button in the queue
    public static string PeekInput()
    {
        return inputButtons.Peek();
    }
}
