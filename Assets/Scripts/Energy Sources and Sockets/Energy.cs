using UnityEngine;
using System;

/*
 * CLASS Energy
 * ------------
 * A unit of artificial energy. Energy as a power level,
 * a type, and a tag identifying the energy
 * ------------
 */ 

[Serializable]
public class Energy : IComparable<Energy>
{
    [SerializeField]
    private int _power;
    [SerializeField]
    private EnergyType _type;
    [SerializeField]
    private Tag _tag;

    // Getters allow read-only access to local vars
    public int power { get { return _power; } }
    public EnergyType type { get { return _type; } }
    public Tag tag { get { return _tag; } }

    public Energy (int e, Collider2D h, EnergyType t, Tag i)
    {
        _power = e;
        _type = t;
        _tag = i;
    }

    public Energy (Energy other)
    {
        _power = other.power;
        _type = other.type;
        _tag = other.tag;
    }

    // Comparison sorts info from least to most power
    public int CompareTo(Energy other)
    {
        if(other == this)
        {
            return 0;
        }
        return _power - other._power;
    }
}

public enum EnergyType
{
    Physical,
    Fire,
    Water,
    Earth,
    Wind,
    Electric,
    Light,
    Dark
}

public enum Tag
{
    Red,
    Yellow,
    Green,
    Cyan,
    Blue,
    Magenta,
    Black,
    White,
}
