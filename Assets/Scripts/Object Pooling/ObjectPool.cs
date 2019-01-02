using System;
using System.Collections.Generic;
using UnityEngine;

/*
 * CLASS ObjectPool<T>
 * -------------------
 * Constructing an object of this class causes multiple copies of a game object
 * (or each game object in a list, depending on the constructor used) to be instantiated
 * into the scene.  
 * 
 * The generic parameter T represents the components on the objects
 * that will be stored in the local member variable list.  
 * 
 * Provides additional functionality, such as a gettable property that gives the
 * calling method a component on an inactive game object in the object pool
 * -------------------
 */ 

public class ObjectPool<T> where T : Component
{
    private List<T> pool = new List<T>();   // List of components attached to game objects intantiated
    private int index = 0;  // Internal index used to get the next available object in the pool

    // Constructor instantes all prefabs in the pool as children of the given game object
    public ObjectPool (PoolData data, Transform parent)
    {
        InstantiatePool(data, parent);
    }

    // Constructor instantiates multiple clones of the given prefab and stores components from each
    public ObjectPool (PoolData data, string parentName = "Object Pool")
    {
        Transform parentTrans = new GameObject(parentName).transform;  // Transform component containing the object pool
        InstantiatePool(data, parentTrans);
    }

    // Override of the method above that instantiates each object in a list,
    // rather than multiple of the same object
    public ObjectPool (List<GameObject> prefabPool, string newParentName = "Object Pool")
    {
        // Make a parent transform by creating a new game object
        Transform parentTrans = new GameObject(newParentName).transform;
        InstantiatePool(prefabPool, parentTrans);
    } // END method

    // Quick property is efficient but won't check any conditions about the object returned
    public T getOne
    {
        get
        {
            // Update current index
            ++index;
            index %= pool.Count;
            return pool[index];
        }
    }

    // Indexer allows calling method to grab a specific object in the pool
    public T this[int index]
    {
        get { return pool[index]; }
    }

    public int Count { get { return pool.Count; } }

    // Allows calling method to get an object that satisfies a condition
    public T GetOne(Predicate<T> predicate)
    {
        // Try to find an object with the given boolean
        T objectGotten = pool.Find(predicate);

        // Log a warning if no object is found
        if (objectGotten == null)
        {
            Debug.LogWarning("Could not find an object that satisfies the predicate");
        }

        return objectGotten;
    }

    // Enable/disable all objects in the pool
    public void SetPoolActive (bool active)
    {
        foreach (T component in pool)
        {
            component.gameObject.SetActive(active);
        }
    }

    // Helper function makes writing unique constructors easier
    private void InstantiatePool(PoolData data, Transform parentTrans)
    {
        GameObject instance;

        // Loop from 1 up to "instances", instantiating prefabs and putting instances into the local list
        for (int count = 1; count <= data.instances; count++)
        {
            instance = UnityEngine.Object.Instantiate(data.prefab, parentTrans);
            instance.name += ("_" + count);
            pool.Add(instance.GetComponent<T>());
        } // END for
    }

    // Helper function for instantiating each object in the list of prefabs
    private void InstantiatePool(List<GameObject> prefabPool, Transform parentTrans)
    {
        GameObject instance;    // Instance of the game object being instantiated

        // Instantiate a copy of every prefab in the list
        foreach (GameObject prefab in prefabPool)
        {
            instance = UnityEngine.Object.Instantiate(prefab, parentTrans);
            pool.Add(instance.GetComponent<T>());
        } // END foreach
    }
}
