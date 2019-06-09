using UnityEngine;

public class ParticleEffectTimingSpecifier
{
    [SerializeField]
    [Tooltip("Particle effect to enable/disable")]
    private ParticleEffect effect;

    // ARGS = 0
    public void EnableEffect()
    {
        effect.EnableEffect();
    }
    public void DisableEffect()
    {
        effect.DisableEffect();
    }
    // ARGS = 1
    public void EnableEffect<T>(T arg)
    {
        effect.EnableEffect();
    }
    public void DisableEffect<T>(T arg)
    {
        effect.DisableEffect();
    }
}
