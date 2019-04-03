using UnityEngine;
using System.Collections;

[System.Serializable]
public class Pair<TOne, TTwo>
{
    [SerializeField]
    [Tooltip("The first object in the pair")]
    TOne _object1;
    [Tooltip("The second object in the pair")]
    [SerializeField]
    TTwo _object2;

    public TOne object1 { get { return _object1; } }
    public TTwo object2 { get { return _object2; } }

    public Pair(TOne one, TTwo two)
    {
        _object1 = one;
        _object2 = two;
    }
}
