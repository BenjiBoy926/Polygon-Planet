using UnityEngine;
using UnityEngine.Events;

public class DelayedUnityEvent : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The event invoked when this unity event is invoked")]
    private UnityEvent _unityEvent;
    public UnityEvent unityEvent { get { return _unityEvent; } }

    public void InvokeDelayed(float delay)
    {
        CancelInvoke();
        Invoke("InvokeEvent", delay);
    }

    private void InvokeEvent()
    {
        _unityEvent.Invoke();
    }
}
