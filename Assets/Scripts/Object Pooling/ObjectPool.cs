using System.Collections;
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

    // Constructor instances multiple clones of the given prefab and stores components from each
    public ObjectPool (PoolData data, string parentName = "Object Pool")
    {
        // List of game objects to be returned
        GameObject instance;    // Reference to instance of newly instantiated prefab
        Transform parentTrans;  // Transform component containing the object pool

        // Create parent transform with the specified name
        parentTrans = new GameObject(parentName).transform;

        // Loop from 1 up to "instances", instantiating prefabs and putting instances into the local list
        for (int count = 1; count <= data.instances; count++)
        {
            instance = Object.Instantiate(data.prefab, parentTrans);
            instance.name += ("_" + count);
            pool.Add(instance.GetComponent<T>());
        } // END for
    }

    // Override of the method above that instantiates each object in a list,
    // rather than multiple of the same object
    public ObjectPool (List<GameObject> prefabPool, string newParentName = "Object Pool")
    {
        GameObject instance;    // Instance of the game object being instantiated

        // Make a parent transform by creating a new game object
        Transform parentTrans = new GameObject(newParentName).transform;

        // Instantiate a copy of every prefab in the list
        foreach (GameObject prefab in prefabPool)
        {
            instance = Object.Instantiate(prefab, parentTrans);
            pool.Add(instance.GetComponent<T>());
        } // END foreach
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

    // Enable/disable all objects in the pool
    public void SetPoolActive (bool active)
    {
        foreach (T component in pool)
        {
            component.gameObject.SetActive(active);
        }
    }
}
