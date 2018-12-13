using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
    [SerializeField]
    private BlowbackMover2D mover;

    private void Start()
    {
        mover.Blowback(Vector2.left, 10f);
    }
}
