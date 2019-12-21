using UnityEngine;
using UnityEngine.Events;

/*
 * CLASS TriggerEvent
 * ------------------
 * Simple class that exposes public events that are invoked on each
 * major physics trigger event
 * ------------------
 */ 

public class TriggerEvent : MonoBehaviour
{
    public event UnityAction<Collider2D> triggerEnteredEvent;
    public event UnityAction<Collider2D> triggerExitedEvent;
    public event UnityAction<Collider2D> triggerStayedEvent;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SafeInvokeTriggerEvent(collision, triggerEnteredEvent);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        SafeInvokeTriggerEvent(collision, triggerStayedEvent);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        SafeInvokeTriggerEvent(collision, triggerExitedEvent);
    }
    private void SafeInvokeTriggerEvent(Collider2D collision, UnityAction<Collider2D> method)
    {
        if(method != null)
        {
            method(collision);
        }
    }
}
