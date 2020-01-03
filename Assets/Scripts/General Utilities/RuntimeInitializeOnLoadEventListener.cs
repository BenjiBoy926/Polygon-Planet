using UnityEngine;
using UnityEngine.Events;

public class RuntimeInitializeOnLoadEventListener : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Event raised at runtime initialize on load")]
    private UnityEvent onLoadEvent;

    private void Awake()
    {
        RuntimeInitializeOnLoadEventRaiser.onLoadEvent.AddListener(onLoadEvent.Invoke);
    }
}
