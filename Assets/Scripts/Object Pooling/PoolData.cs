using UnityEngine;
using UnityEditor;

/*
 * STRUCT PoolData
 * ---------------
 * Groups together a game object prefab and the number of instances
 * that will be instantiated in an object pool
 * ---------------
 */ 
 [System.Serializable]
public struct PoolData
{
    [SerializeField]
    GameObject _prefab;
    [SerializeField]
    int _instances;

    public GameObject prefab { get { return _prefab; } }
    public int instances { get { return _instances; } }

    public PoolData (GameObject obj, int inst)
    {
        _prefab = obj;
        _instances = inst;
    }
}