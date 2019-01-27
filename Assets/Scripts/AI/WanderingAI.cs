using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * CLASS WanderingAI
 * -----------------
 * Given a type of mover, this script causes the mover to 
 * wander around aimlessly up, down, left or right
 * -----------------
 */ 

public class WanderingAI : MonoBehaviour
{
    [SerializeField]
    private KinematicMover2D agent;  // Script that moves the agent around
    [SerializeField]
    private float speed; // Speed the ai moves in
    [SerializeField]
    private DirectionalType directions; // Determines what directions the ai is allowed to wander in
    [SerializeField]
    private IntConstraint wanderTimes;    // Min-max times for which the ai can wander in one direction before changing directions

    private WaitUntil WaitUntil;    // Wait command used in all wandering coroutines
    private List<Vector2> availableDirections = new List<Vector2>();  // List of directions the ai can wander in
    private State isWandering;  // State is true if the ai is still wandering in the direction most recently selected

    private void Start()
    {
        isWandering = State.Construct(obj: gameObject);

        // Add correct vectors to the list
        if(directions == DirectionalType.AllDirections)
        {
            AddHorizontalDirections();
            AddVerticalDirections();
        }
        else if(directions == DirectionalType.HorizontalOnly)
        {
            AddHorizontalDirections();
        }
        else
        {
            AddVerticalDirections();
        }

        // Initialize wait command and start wandering
        WaitUntil = new WaitUntil(HasFinishedWandering);
        StartWandering();
    }

    public void StartWandering()
    {
        StopAllCoroutines();

        if(availableDirections.Count > 2)
        {
            StartCoroutine("GeneralWander");
        }
        else
        {
            StartCoroutine("SwitchWander");
        }
    }
    public void StopWandering()
    {
        agent.Stop();
        StopAllCoroutines();
    }

    // General version of the wandering AI that selects between four different possible directions
    private IEnumerator GeneralWander()
    {
        int currentDirectionIndex;  // Index in list of available directions currently wandering in
        int prevoiusDirectionIndex = -1; // Index of the last direction the AI moved in
        int wanderTime;

        while(true)
        {
            // Loop until an index is randomly selected that is different from the previous index
            do
            {
                currentDirectionIndex = Random.Range(0, availableDirections.Count);
            } while (currentDirectionIndex == prevoiusDirectionIndex);

            // Make agent move and set is wandering state to random int within range
            agent.MoveTowards(availableDirections[currentDirectionIndex], speed);
            wanderTime = Random.Range(wanderTimes.min, wanderTimes.max + 1);
            isWandering.Activate(wanderTime);

            yield return WaitUntil;

            // Previous direction index is now current direction index
            prevoiusDirectionIndex = currentDirectionIndex;
        }
    }

    // A specified version of the wandering AI that optimizes under the assumptio that there are only two directions available
    private IEnumerator SwitchWander()
    {
        // Used to select what direction the AI will move in next
        // Either starts with (1,0,1,0,...) or (0,1,0,1,...)
        int directionSelector = Random.Range(0, 2);
        int wanderTime; // Time for which the AI will wander in the current direction

        while(true)
        {
            // Make the agent move, randomly generate a wander time and set the state
            agent.MoveTowards(availableDirections[directionSelector], speed);
            wanderTime = Random.Range(wanderTimes.min, wanderTimes.max + 1);
            isWandering.Activate(wanderTime);

            yield return WaitUntil;

            // Increment the selector, then mod it by two
            ++directionSelector;
            directionSelector %= 2;
        }
    }

    // Used in the WaitUntil command in the wander coroutine
    private bool HasFinishedWandering()
    {
        return !isWandering || agent.rb2D.velocity.sqrMagnitude < float.Epsilon;
    }

    private void AddHorizontalDirections()
    {
        availableDirections.Add(Vector2.left);
        availableDirections.Add(Vector2.right);
    }
    private void AddVerticalDirections()
    {
        availableDirections.Add(Vector2.up);
        availableDirections.Add(Vector2.down);
    }
}

public enum DirectionalType
{
    AllDirections,
    HorizontalOnly,
    VerticalOnly
}