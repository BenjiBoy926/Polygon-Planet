using UnityEngine;
using System.Collections;

/*
 * CLASS TaggedPoolData
 * --------------------
 * Simple class pairs a string tag with a set of object pooling data
 * --------------------
 */ 

[System.Serializable]
public class TaggedPoolData
{
    [SerializeField]
    private string _tag;
    [SerializeField]
    private PoolData _data;
    
    public string tag { get { return _tag; } }
    public PoolData data { get { return _data; } }
}
