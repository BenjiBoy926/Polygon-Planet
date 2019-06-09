using UnityEngine;

public class EnergySocketEffector : EventEffector<EnergySocket>
{
    [SerializeField]
    [Tooltip("Effect enabled when the socket absorbs positive energy")]
    private ParticleEffect energyIncreasedEffect;
    [SerializeField]
    [Tooltip("Effect enabled when the socket absorbs negative energy")]
    private ParticleEffect energyDecreasedEffect;
    [SerializeField]
    [Tooltip("Effect enabled when the socket absorbs any energy level")]
    private ParticleEffect energyAbsorbedEffect;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        //energyIncreasedEffect.Initialize();
        //energyDecreasedEffect.Initialize();
        //energyAbsorbedEffect.Initialize();
        //eventHandle._energyAbsorbedEvent += Effect;
    }

    private void Effect(EnergyAbsorbedEventData data)
    {
        if(data.amountAbsorbed < 0)
        {
            //energyDecreasedEffect
        }
    }
}
