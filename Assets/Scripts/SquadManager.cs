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
    private const float SIDE_BUFF = 0.7f;   // When instantiating a squad member next to one side of the stage, this space is left between the member and the side

    [SerializeField]
    private List<SquadMember> squad;    // The list of squad members to instantiate on loading up
    private Bounds boundingBox; // Valid area within which squad members can be instantiated

    private void Start()
    {
        boundingBox = Boundary.instance.bounds;
        InstantiateSquad();
    }

    private void InstantiateSquad()
    {
        Vector3 memberPos;  // The position of the next squad member to instantiate

        foreach(SquadMember member in squad)
        {
            memberPos = GenerateSquadMemberPosition(member);
            Instantiate(member.prefab, memberPos, member.prefab.transform.rotation);
        }
    }

    private Vector3 GenerateSquadMemberPosition(SquadMember member)
    {
        Vector3 memberPos = new Vector3();

        if (member.positionType == PositionInstantiationType.OnCeiling || 
            member.positionType == PositionInstantiationType.OnFloor)
        {
            memberPos.x = Random.Range(boundingBox.min.x + SIDE_BUFF, boundingBox.max.x - SIDE_BUFF);

            if(member.positionType == PositionInstantiationType.OnCeiling)
            {
                memberPos.y = boundingBox.max.y - SIDE_BUFF;
            }
            else
            {
                memberPos.y = boundingBox.min.y + SIDE_BUFF;
            }
        }
        else if (member.positionType == PositionInstantiationType.OnLeftWall || 
            member.positionType == PositionInstantiationType.OnRightWall)
        {
            memberPos.y = Random.Range(boundingBox.min.y + SIDE_BUFF, boundingBox.max.y - SIDE_BUFF);

            if (member.positionType == PositionInstantiationType.OnLeftWall)
            {
                memberPos.x = boundingBox.min.x + SIDE_BUFF;
            }
            else
            {
                memberPos.x = boundingBox.max.x - SIDE_BUFF;
            }
        }
        else
        {
            memberPos.x = Random.Range(boundingBox.min.x + SIDE_BUFF, boundingBox.max.x - SIDE_BUFF);
            memberPos.y = Random.Range(boundingBox.min.y + SIDE_BUFF, boundingBox.max.y - SIDE_BUFF);
        }

        return memberPos;
    }
}
