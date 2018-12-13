using UnityEngine;
using UnityEditor;

public interface ISingleStateObject
{
    State primaryState { get; }
}