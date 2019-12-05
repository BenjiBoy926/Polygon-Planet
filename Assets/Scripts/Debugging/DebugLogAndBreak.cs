using UnityEngine;

public class DebugLogAndBreak : MonoBehaviour
{
    public void LogAndBreak(string message)
    {
        Debug.Log(message);
        Debug.Break();
    }
}
