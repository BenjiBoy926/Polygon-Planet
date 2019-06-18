using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public abstract class Task : MonoBehaviour
{
    /*
     * PUBLIC DATA
     */

    [SerializeField]
    [Tooltip("When the task starts in the timeline")]
    private FloatConstraint startTimeRange;
    [SerializeField]
    [Tooltip("The number of times the task function is called when it starts")]
    private int numberOfCalls;
    [SerializeField]
    [Tooltip("Amount of time between each call of the task")]
    private float timeBetweenCalls;

    [SerializeField]
    [Tooltip("Time before starting the task run cycle that the opening event is called")]
    private float openingEventTime;
    [SerializeField]
    [Tooltip("Event called a certain time before running the task cycle")]
    private UnityEvent _openingEvent;
    public UnityEvent openingEvent { get { return _openingEvent; } }

    [SerializeField]
    [Tooltip("Time after finishing the task run cycle that the closing event is called")]
    private float closingEventTime;
    [SerializeField]
    [Tooltip("Event called a certain time after the task cycle finishes")]
    private UnityEvent _closingEvent;
    public UnityEvent closingEvent { get { return _closingEvent; } }

    // Start time selected for this task's run cycle
    public float startTime { get; private set; }
    // True if the task run cycle has begun
    public bool started { get; private set; }

    /*
     * PUBLIC INTERFACE
     */

    public abstract void Run();

    public void StopTask()
    {
        started = false;
        startTime = Random.Range(startTimeRange.min, startTimeRange.max);
        StopAllCoroutines();
    }

    public void StartTask()
    {
        if (!started)
        {
            StopAllCoroutines();
            StartCoroutine("RunTask");
        }
        started = true;
    }

    public float LatestPossibleStartTime()
    {
        return openingEventTime + startTimeRange.max + closingEventTime;
    }

    /*
     * PRIVATE HELPERS
     */

    private IEnumerator RunTask()
    {
        WaitForSeconds cycleDelay = new WaitForSeconds(timeBetweenCalls);

        // Invoke the opening event, then wait the seconds given
        _openingEvent.Invoke();
        yield return new WaitForSeconds(openingEventTime);

        // Repeatedly run the task for the number of calls specified
        for (int cycle = 0; cycle < numberOfCalls; cycle++)
        {
            Run();

            if (cycle < numberOfCalls - 1)
            {
                yield return cycleDelay;
            }
        }

        // Wait the seconds given, then invoke the closing event
        yield return new WaitForSeconds(closingEventTime);
        _closingEvent.Invoke();
    }
}