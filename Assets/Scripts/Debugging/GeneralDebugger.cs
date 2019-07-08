using UnityEngine;
using System.Collections;

public class GeneralDebugger : MonoBehaviour
{
    public void Log(string message)
    {
        Debug.Log(message);
    }
    public void Break()
    {
        Debug.Break();
    }
}
