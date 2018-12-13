using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * CLASS DamageInfo
 * ----------------
 * Pairs a strength integer value with the collider of the offending object
 * ----------------
 */ 

[Serializable]
public class ProjectileInfo : IComparable<ProjectileInfo>
{
    [SerializeField]
    private int _strength;
    [SerializeField]
    private Collider2D _hitBox;

    public int strength { get { return _strength; } }
    public Collider2D hitBox { get { return _hitBox; } }

    public ProjectileInfo (int str, Collider2D col)
    {
        _strength = str;
        _hitBox = col;
    }

    // Comparison method organizes info from most to least damaging
    public int CompareTo (ProjectileInfo other)
    {
        if (other == this)
        {
            return 0;
        }
        return other._strength - _strength;
    }
}