using UnityEngine;

public class EnergySourceEffector : EventEffector<EnergySource>
{
    [SerializeField]
    [Tooltip("Particle effect enabled when the energy source transfers its energy")]
    private ParticleEffect energyTransferredEvent;

    protected override void Start()
    {
        base.Start();
        //energyTransferredEvent.Initialize();
        //eventHandle.energyTransferredEvent += (arg) => energyTransferredEvent.EnableEffect();
    }
}
