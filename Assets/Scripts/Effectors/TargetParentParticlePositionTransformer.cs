public class TargetParentParticlePositionTransformer : TargetParticlePositionTransformer
{
    // Start is called before the first frame update
    void Start()
    {
        target = transform.parent;
    }
}
