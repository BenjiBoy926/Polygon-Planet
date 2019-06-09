using UnityEngine;

public class FixedParticlePositionTransformer : ParticleEffectTransformer
{
    [SerializeField]
    [Tooltip("Position that the particle is moved to whenever it is enabled")]
    private Vector3 position;
    [SerializeField]
    [Tooltip("Space in which the position is interpreted")]
    private Space positionSpace;

    protected override void ParticleTransformer(Transform particle)
    {
        if(positionSpace == Space.World)
        {
            particle.position = position;
        }
        else
        {
            particle.localPosition = position;
        }
    }
}
