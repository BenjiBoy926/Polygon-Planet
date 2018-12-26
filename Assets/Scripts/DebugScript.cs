using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
    [SerializeField]
    private Shaker shaker;

    private void Start()
    {
        shaker.Shake(time: 10f);
    }
}
