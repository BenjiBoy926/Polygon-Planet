using UnityEngine;
using UnityEngine.Events;

public class DebugScript : MonoBehaviour
{
    [System.Serializable] public class IntEvent : UnityEvent<int> { };
    public IntEvent unityEvent;
}
