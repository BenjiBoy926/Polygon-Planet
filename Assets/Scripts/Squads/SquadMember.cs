using UnityEngine;
using UnityEditor;

/*
 * CLASS SquadMember
 * -----------------
 * Defines a single member of a squad
 * -----------------
 */ 
[System.Serializable]
public class SquadMember
{
    [SerializeField]
    private GameObject _prefab;  // A clone of this object is instantated into the scene to represent the squad member
    [SerializeField]
    private PositionInstantiationType _positionType; // Defines the rule the squad manager uses to place this squad member
    [SerializeField]
    private Vector2 _fixedPos;   // The fixed position that this object is instantiated at

    public GameObject prefab { get { return _prefab; } }
    public PositionInstantiationType positionType { get { return _positionType; } }
    public Vector2 fixedPos { get { return _fixedPos; } }

    public void InstantiateSelf(Vector3 pos)
    {
        Object.Instantiate(_prefab, pos, _prefab.transform.rotation);
    }
}

public enum PositionInstantiationType
{
    Fixed,
    Random,
    OnRightWall,
    OnLeftWall,
    OnFloor,
    OnCeiling
}