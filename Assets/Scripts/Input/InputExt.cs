using UnityEngine;
using System.Collections;

public static class InputExt
{
    public static bool GetButton(string buttonName, InputButtonType type)
    {
        switch(type)
        {
            case InputButtonType.Up:
                return Input.GetButtonUp(buttonName);
            case InputButtonType.Down:
                return Input.GetButtonDown(buttonName);
            case InputButtonType.Stay:
                return Input.GetButton(buttonName);
            default:
                throw new System.Exception("For input enum " + type);
        }
    }
}
