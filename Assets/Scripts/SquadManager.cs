using UnityEngine;
using System.Collections.Generic;

/*
 * CLASS SquadManager
 * ------------------
 * A squad manager defines an aggregation of squad members 
 * that are automatically instantiated into the scene from the start
 * ------------------
 */ 

public class SquadManager : MonoBehaviour
{
    [SerializeField]
    private List<SquadMember> squad;

    private void Start()
    {
        
    }
}
