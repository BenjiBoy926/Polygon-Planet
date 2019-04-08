using UnityEngine;
using System.Collections;

// Encapsulates data related to when an energy source is nullified
[System.Serializable]
public class EnergyNullifiedEventData : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Reference to the script that nullified an energy source")]
    private EnergySourceNullifier _nullifyer;
    [SerializeField]
    [Tooltip("Reference to the script that was nullified by the nullifier")]
    private EnergySource _source;

    public EnergySourceNullifier nullifyer { get { return _nullifyer; } }
    public EnergySource source { get { return _source; } }

    public EnergyNullifiedEventData(EnergySourceNullifier energyNullifier, EnergySource energySource)
    {
        _nullifyer = energyNullifier;
        _source = energySource;
    }
}
