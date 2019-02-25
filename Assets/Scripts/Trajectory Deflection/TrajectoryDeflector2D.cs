using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * CLASS TrajectorDeflector2D
 * --------------------------
 * Integrates a series of deflector walls. It keeps the normals of the walls
 * accurate by accounting for the rotation of the deflector, assuming that the
 * walls are children of the deflector's transform
 * --------------------------
 */ 

public class TrajectoryDeflector2D : MonoBehaviour
{
    [SerializeField]
    private List<DeflectorWall2D> deflectors;   // A list of the walls associated with this deflector

    // Start is called before the first frame update
    void Start()
    {

    }
}
