using UnityEngine;
using UnityEngine.Events;

public class EnableEvents : MonoBehaviour
{
    public event UnityAction enableEvent;
    public event UnityAction disableEvent;

    private void OnEnable()
    {
        if(enableEvent != null)
        {
            enableEvent();
        }
    }

    private void OnDisable()
    {
        if(disableEvent != null)
        {
            disableEvent();
        }
    }
}
