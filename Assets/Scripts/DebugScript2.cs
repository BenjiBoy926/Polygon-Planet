using UnityEngine;
using System.Collections;

public class DebugScript2 : MonoBehaviour
{
    [SerializeField]
    private TransformMover mover;
    [SerializeField]
    private Vector2 point;
    [SerializeField]
    private float time;
    [SerializeField]
    private Space space;

    private void Start()
    {
        mover.MoveToPoint2D(point, time, space);
    }
}
