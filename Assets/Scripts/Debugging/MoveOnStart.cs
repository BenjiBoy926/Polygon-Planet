using UnityEngine;
using System.Collections;

public class MoveOnStart : MonoBehaviour
{
    [SerializeField]
    private KinematicMover2D mover;
    [SerializeField]
    private Vector2 direction;
    [SerializeField]
    private float speed;
    // Use this for initialization
    void Start()
    {
        mover.MoveTowards(direction, speed);
    }   
}
