using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

/*
 * CLASS DeathEventHandler
 * -----------------------
 * A simple base class for classes the create events when certain objects die
 * Given a list of tags, it tries to find death handlers on those gameobjects
 * and subscribe the desired method to each of their death events
 * -----------------------
 */ 

public class DeathEventHandler : MonoBehaviour
{
    [SerializeField]
    private List<string> deathObjectTags;  // Tag of the objects with the death handlers on them
    protected List<IDeathHandler> deathHandles = new List<IDeathHandler>();

    protected void SubscribeToDeathEvents(UnityAction method)
    {
        foreach (string tag in deathObjectTags)
        {
            // Array of objects expected to have death handlers on them 
            GameObject[] deathObjects = GameObject.FindGameObjectsWithTag(tag);
            IDeathHandler currentHandler;   // Death handler on the current object in the array

            // Get death handlers on each of the objects
            foreach (GameObject obj in deathObjects)
            {
                currentHandler = obj.GetComponentInChildren<IDeathHandler>();

                // If the object has a death handler, add it to the list
                if (currentHandler != null)
                {
                    currentHandler.AddDeathEvent(method);
                    deathHandles.Add(currentHandler);
                }
            }
        }
    }
}
