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

    public GameObject prefab { get { return _prefab; } }
}

public enum PositionInstantiationType
{
    Random,
    OnRightWall,
    OnLeftWall,
    OnFloor,
    OnCeiling
}