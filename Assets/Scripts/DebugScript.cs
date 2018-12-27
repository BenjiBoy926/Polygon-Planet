using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
    [SerializeField]
    private Shaker shaker;

    private void Start()
    {
        shaker.Shake(5f, 10f);
    }
}
