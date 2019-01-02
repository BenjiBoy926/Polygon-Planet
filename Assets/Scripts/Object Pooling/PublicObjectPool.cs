using UnityEngine;
using System.Collections.Generic;

/*
 * CLASS PublicObjectPool
 * ----------------------
 * Public object pools pair readable tags with object pools
 * and allow client code to access objects in the object pools
 * that have the specified tags associated with them
 * ----------------------
 */ 

public class PublicObjectPool : MonoBehaviour
{
    [SerializeField]
    private List<TaggedPoolData> poolDatas;  // List of object pool data combined with a string tag
    // Dictionary of object pools combines tags from the tagged pool data with the corresponding object pools
    private Dictionary<string, ObjectPool<Transform>> objectPools = new Dictionary<string, ObjectPool<Transform>>();

    private void Start()
    {
        foreach(TaggedPoolData data in poolDatas)
        {
            Transform poolParent = new GameObject(data.tag).transform;
            poolParent.parent = transform;
            objectPools.Add(data.tag, new ObjectPool<Transform>(data.data, poolParent));
            objectPools[data.tag].SetPoolActive(false);
        }
    }

    // Get a transform with the specified tag
    public Transform GetOne(string tag)
    {
        if(objectPools.ContainsKey(tag))
        {
            return objectPools[tag].getOne;
        }
        else
        {
            Debug.Log("Public object pool on " + gameObject.name + " does not have an object pool tagged " + tag);
            return null;
        }
    }
}
