using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * STRUCT Anchor
 * -------------
 * Structure pairs two x-y coordinates - one to represent
 * an origin and the other to represent a direction
 * -------------
 */ 
 [System.Serializable]
public struct Anchor
{
    public Vector2 origin;
    public Vector2 direction;

    public Anchor (Vector2 org, Vector2 dir)
    {
        origin = org;
        direction = dir;
    }
}
