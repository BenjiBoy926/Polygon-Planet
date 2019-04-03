using UnityEngine;
using System.Collections;

/*
 * CLASS ReportMuteAndUnmuteColliders
 * ----------------------------------
 * Simple debugging class reports muting and unmuting that occurs on a 
 * "OneTriggerPerLifetime"
 * ----------------------------------
 */ 

public class ReportMuteAndUnmuteColliders : MonoBehaviour
{
    [SerializeField]
    private OneTriggerPerLifetime triggerHandler;
    private void Start()
    {
        triggerHandler.colliderMutedEvent += ReportMute;
        triggerHandler.colliderUnmutedEvent += ReportUnmute;
    }
    private void ReportMute(Collider2D collider)
    {
        Debug.Log("Trigger handler on " + gameObject.name + " muted collider named " + collider.name);
    }
    private void ReportUnmute(Collider2D collider)
    {
        Debug.Log("Trigger handler on " + gameObject.name + " unmuted collider named " + collider.name);
    }
}
