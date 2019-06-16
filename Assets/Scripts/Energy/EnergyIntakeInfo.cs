using UnityEngine;

/*
 * CLASS EnergyIntakeInfo
 * ----------------------
 * Pairs an energy type with a multiplier. Energy sockets can use this information
 * to change how much energy is actually absorbed from an energy source
 * ----------------------
 */ 

[System.Serializable]
public class EnergyIntakeInfo
{
    [SerializeField]
    private float _multiplier;
    public float multiplier
    {
        get { return _multiplier; }
        set { _multiplier = value; }
    }
    [SerializeField]
    private EnergyType _type;
    public EnergyType type { get { return _type; } }

    public EnergyIntakeInfo(EnergyType t, float m)
    {
        _type = t;
        _multiplier = m;
    }
}
