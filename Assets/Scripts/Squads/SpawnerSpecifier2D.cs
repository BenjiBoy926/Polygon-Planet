using UnityEngine;

public class SpawnerSpecifier2D : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The prefab to spawn an instance of")]
    private GameObject original;

    [SerializeField]
    [Tooltip("Tag of the game object that will be used as the boundary " +
        "within which the object is instantiated. The boundary object is " +
        "expected to have a trigger on it, and the spawner uses the bounding " +
        "box of the trigger collider")]
    private string boundaryTag;

    [SerializeField]
    [Tooltip("Margin within the bounding box that random selections stay within")]
    private Vector2 margin;

    [SerializeField]
    [Tooltip("Whether the x position is random within the boundary or fixed")]
    private SpawnDimensionType xPositionType;

    [SerializeField]
    [Tooltip("Fixed x position")]
    private float xPosition;

    [SerializeField]
    [Tooltip("Whether the y position is random within the boundary or fixed")]
    private SpawnDimensionType yPositionType;

    [SerializeField]
    [Tooltip("Fixed y position")]
    private float yPosition;

    private LazyLoader<Collider2D> boundsTrigger;

    public void Spawn()
    {
        if(boundsTrigger == null)
        {
            boundsTrigger = new LazyLoader<Collider2D>(() =>
            {
                return GameObject.FindGameObjectWithTag(boundaryTag).GetComponent<Collider2D>();
            });
        }
        Instantiate(original, GeneratePosition(), original.transform.rotation);
    }

    private Vector3 GeneratePosition()
    {
        return new Vector3(GenerateX(), GenerateY(), original.transform.position.z);
    }
    private float GenerateX()
    {
        return GenerateValue(xPositionType, xPosition, boundsTrigger.obj.bounds.min.x, boundsTrigger.obj.bounds.max.x, margin.x);
    }
    private float GenerateY()
    {
        return GenerateValue(yPositionType, yPosition, boundsTrigger.obj.bounds.min.y, boundsTrigger.obj.bounds.max.y, margin.y);
    }
    private float GenerateValue(SpawnDimensionType type, float fixedSize, float minDimension, float maxDimension, float marginDimension)
    {
        switch(type)
        {
            case SpawnDimensionType.Fixed: return fixedSize;
            case SpawnDimensionType.FixedToMin: return minDimension + marginDimension;
            case SpawnDimensionType.FixedToMax: return maxDimension - marginDimension;
            default: return Random.Range(minDimension + marginDimension, maxDimension - marginDimension);
        }
    }
}

public enum SpawnDimensionType { Fixed, FixedToMin, FixedToMax, Random }