using System;
using System.Collections.Generic;
using UnityEngine;

/*
 * CLASS ObjectPool<T>
 * -------------------
 * Gives client code access to a recyclable list of components on
 * the GameObject prefab specified
 * 
 * Client code can use quick properties to get an object 
 * that is NOT currently active in the scene 
 * 
 * The object pool dynamically grows if a request is made for an inactive object
 * that cannot be satisfied
 * 
 * For additional control, client code can request an object that 
 * satisfies a given condition other than that the object is inactive
 * This does not cause the pool to grow, since there is no guarantee
 * that the new object would itself satisfy the condition
 * 
 * The generic parameter T represents the components on the objects
 * that will be stored in the local member variable list.  
 * -------------------
 */ 

public class ObjectPool<T> where T : Component
{
    // Game object instantiated into the pool. It MUST have a component of type T either on it or one of the child GameObjects
    private GameObject prefab;
    private Transform poolParent;   // Transform under which all objects are instantiated as children

    private List<T> pool = new List<T>();   // List of components attached to game objects intantiated
    private int index = 0;  // Internal index used to get the next available object in the pool

    /*
     * CONSTRUCTORS
     * ------------
     */ 

    // Constructors start the pool with only one object in the pool
    // Focused on dynamically growing at runtime to decrease overhead at the startup
    public ObjectPool(GameObject obj, Transform parent)
    {
        InitializeData(obj, parent);
        AddOne();
    }
    public ObjectPool(GameObject obj, string parentName = "Object Pool")
    {
        InitializeData(obj, parentName);
        AddOne();
    }


    // Constructors start the pool off with the number of copies specified by the pool data
    // Focused on statically sized pools that decrease overhead at runtime
    public ObjectPool (PoolData data, Transform parent)
    {
        InitializeData(data.prefab, parent);
        InitializePool(data);
    }
    public ObjectPool(PoolData data, string parentName = "Object Pool")
    {
        InitializeData(data.prefab, parentName);
        InitializePool(data);
    }

    /*
     * PUBLIC FUNCITONS
     * ----------------
     */ 

    // Request an object from the pool using quick indexing
    // This should be used if the objects are expected to be active
    // for roughly the same amount of time. Large disparities in lifetimes
    // could result in unnecessarily large object pools being allocated
    public T getOneQuick
    {
        get
        {
            // Advance the index
            index = (index + 1) % pool.Count;

            // If the next object is still active...
            if(pool[index].gameObject.activeInHierarchy)
            {
                //...add and store a new object
                T newObject = AddOne();

                // If the index is at the last object,
                // advance the index again to skip over the object recently added
                if(index == pool.Count - 2)
                {
                    index++;
                }
                
                return newObject;
            }

            return pool[index];
        }
    }

    // Request an object from the pool using absolute finding
    // This guarantees that the object pool only dynamically grows with the needs of the client,
    // at the potential cost of efficiency in using the "Find" algorithm whenever the property is invoked
    public T getOne
    {
        get
        {
            // Try to get an inactive object
            T got = pool.Find(InactiveObject);

            // If no inactive objects exist in the pool, add one and return it
            if(got == null)
            {
                return AddOne();
            }

            return got;
        }
    }

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

    // Indexer allows calling method to grab a specific object in the pool
    public T this[int index]
    {
        get { return pool[index]; }
    }

    public int Count { get { return pool.Count; } }

    /*
     * PRIVATE HELPERS
     * ---------------
     */

    // Instantiate a copy of the locally defined object into the object pool 
    // and keep a reference to the specified component
    // Return the component grabbed
    private T AddOne()
    {
        GameObject instance;    // Instance of the game object being instantiated
        instance = UnityEngine.Object.Instantiate(prefab, poolParent);
        pool.Add(instance.GetComponent<T>());
        instance.SetActive(false);

        // Return that last object that was just added
        return pool[pool.Count - 1];
    }

    // Helper function makes writing unique constructors easier
    private void InitializePool(PoolData data)
    {
        // Loop from 1 up to "instances", instantiating prefabs and putting instances into the local list
        for (int count = 1; count <= data.initialSize; count++)
        {
            AddOne();
        } // END for

        // Pool is initially inactive
        SetPoolActive(false);
    }

    // Enable/disable all objects in the pool
    public void SetPoolActive(bool active)
    {
        foreach (T component in pool)
        {
            component.gameObject.SetActive(active);
        }
    }

    // Intitialize local variables
    private void InitializeData(GameObject obj, Transform parent)
    {
        prefab = obj;
        poolParent = parent;
    }
    private void InitializeData(GameObject obj, string parentName)
    {
        // Construct a parent transform and assign local variables
        Transform parent = new GameObject(parentName).transform;
        prefab = obj;
        poolParent = parent;
    }

    // Returns true if the component specified is no active in the scene
    private bool InactiveObject(T obj)
    {
        return !obj.gameObject.activeInHierarchy;
    }
}
