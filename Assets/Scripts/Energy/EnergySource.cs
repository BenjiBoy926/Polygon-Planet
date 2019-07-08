using UnityEngine;
using UnityEngine.Events;

/*
 * CLASS EnergySource
 * ------------------
 * Generic source of energy transfers energy to any energy socket it
 * comes into physical contact with. Energy sockets interpret the energy
 * and raises an event, passing amount actually absorbed
 * ------------------
 */ 

public class EnergySource : CollisionComponentProcessor<EnergySocket>
{
    /*
     * PUBLIC TYPEDEFS
     */
    [System.Serializable] public class EnergyEvent : UnityEvent<EnergyEventData> { };

    [SerializeField]
    private Energy _energy;
    public Energy energy { get { return _energy; } }
    [SerializeField]
    [Tooltip("Set of events invoked when the source transfers its energy to a socket")]
    private EnergyEvent _energyTransferredEvent;
    public EnergyEvent energyTransferredEvent { get { return _energyTransferredEvent; } }

    // A set of energy that has the original values of the energy
    // at the start of the scene
    private Energy _defaultEnergy;
    private Energy defaultEnergy
    {
        get
        {
            if (_defaultEnergy == null)
            {
                _defaultEnergy = new Energy(_energy);
            }
            return _defaultEnergy;
        }
    }

    private void Start()
    {
        _defaultEnergy = new Energy(_energy);   
    }

    // Default energy whenever the source is enabled
    private void OnEnable()
    {
        _energy = new Energy(defaultEnergy);
    }

    // Transfer energy to the given energy socket and call the event if it exists
    protected override void ProcessComponent(EnergySocket socket)
    {
        int amountAbsorbed = socket.AbsorbEnergy(this);
        energyTransferredEvent.Invoke(new EnergyEventData(this, socket, amountAbsorbed));
    }
    // Scale up or scale down the current energy level of the energy source
    public void ScalePowerLevel(float multiplier)
    {
        int newPower = Mathf.RoundToInt(_energy.power * multiplier);
        _energy = new Energy(newPower, _energy.type, _energy.tag);
    }
    public void SetTag(Tag newTag)
    {
        energy.tag = newTag;
    }
}
