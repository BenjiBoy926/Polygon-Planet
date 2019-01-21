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
    int _initialSize;

    public GameObject prefab { get { return _prefab; } }
    public int initialSize { get { return _initialSize; } }

    public PoolData (GameObject obj)
    {
        _prefab = obj;
        _initialSize = 1;
    }

    public PoolData (GameObject obj, int init)
    {
        _prefab = obj;
        _initialSize = init;
    }
}