using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectUtils
{
    public static GameObject GetAncestor(GameObject ego, int level)
    {
        Transform current = ego.transform;
        for(int i = 0; i < level; i++)
        {
            current = current.parent;
        }
        return current.gameObject;
    }

    public static GameObject GetDescendent(GameObject ego, List<int> childIndeces)
    {
        Transform current = ego.transform;
        foreach(int index in childIndeces)
        {
            current = current.GetChild(index);
        }
        return current.gameObject;
    }
}
