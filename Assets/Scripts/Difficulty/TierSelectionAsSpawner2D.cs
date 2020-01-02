using System.Collections.Generic;
using UnityEngine;

public class TierSelectionAsSpawner2D : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Must point to a GameObject with a component of type ITierSelector attached")]
    private GameObjectReference reference;

    private void Start()
    {
        ITierSelector tierSelector = reference.gameObjectReferenced.GetComponent<ITierSelector>();
        tierSelector.tierSelectionEvent.AddListener(SpawnAll);
    }

    private void SpawnAll(List<Object> tierSelections)
    {
        foreach(Object obj in tierSelections)
        {
            if (obj.GetType() == typeof(GameObject))
            {
                SpawnerSpecifier2D spawner = ((GameObject)obj).GetComponent<SpawnerSpecifier2D>();
                if (spawner != null)
                {
                    spawner.Spawn();
                }
            }
        }
    }
}
