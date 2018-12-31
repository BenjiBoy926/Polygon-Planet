using UnityEngine;
using System.Collections.Generic;

/*
 * CLASS SquadManager
 * ------------------
 * A squad manager defines an aggregation of squad members 
 * that are automatically instantiated into the scene from the start
 * The SquadManager has the additional ability to partially
 * heal members of the squad and even adjust the difficulty 
 * of the members in the squad
 * ------------------
 */ 

public class SquadManager : MonoBehaviour
{
    private const float SIDE_BUFF = 0.7f;   // This space is left between squad members and sides of the bounds

    [SerializeField]
    private string mainBoundaryTag; // Tag of the object with the main boundary on it
    [SerializeField]
    private List<SquadMember> objectList;    // The list of squad members to instantiate on loading up
    private Bounds boundingBox; // Valid area within which squad members can be instantiated

    private void Start()
    {
        // Get the bounding box of the area
        GameObject boundaryObject = GameObject.FindGameObjectWithTag(mainBoundaryTag);
        Boundary boundary = boundaryObject.GetComponent<Boundary>();
        boundingBox = boundary.bounds;

        // Instantiate the squad
        InstantiateSquad();
    }

    private void InstantiateSquad()
    {
        Vector3 memberPos;  // The position of the next squad member to instantiate

        foreach(SquadMember member in objectList)
        {
            memberPos = GenerateSquadMemberPosition(member);
            member.InstantiateSelf(memberPos);
        }
    }

    private Vector3 GenerateSquadMemberPosition(SquadMember member)
    {
        Vector3 memberPos = new Vector3();

        // If the member has a fixed position to be placed at
        if(member.positionType == PositionInstantiationType.Fixed)
        {
            memberPos = member.fixedPos;
        }
        // If the member is on the floor or ceiling, fix y position and randomize x position
        else if (member.positionType == PositionInstantiationType.OnCeiling ||
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
        // If the member is on either wall, fix x position and randomize y position
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
        // If member can be placed randomly, generate random position within bounds
        else
        {
            memberPos.x = Random.Range(boundingBox.min.x + SIDE_BUFF, boundingBox.max.x - SIDE_BUFF);
            memberPos.y = Random.Range(boundingBox.min.y + SIDE_BUFF, boundingBox.max.y - SIDE_BUFF);
        }

        return memberPos;
    }
}
