using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ObjectListEvent : UnityEvent<List<Object>> { };

public interface ITierSelector
{
    ObjectListEvent tierSelectionEvent { get; }
    void TierSelection();
}
