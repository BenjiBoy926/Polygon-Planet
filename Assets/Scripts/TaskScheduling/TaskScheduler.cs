using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * CLASS TaskScheduler
 * -------------------
 * Runs all of the tasks in the list according to data set up in the list
 * -------------------
 */ 

public class TaskScheduler : MonoBehaviour
{
    /*
     * CONSTANTS
     */ 
    public const float REFRESH_INTERVAL = 0.005f;

    /*
     * PUBLIC DATA
     */ 
    [SerializeField]
    [Tooltip("List of tasks to invoke on the schedule")]
    private List<Task> tasks;
    [SerializeField]
    [Tooltip("If true, tasks start as soon as the scene loads")]
    private bool beginOnStart;
    [SerializeField]
    [Tooltip("Time after the latest task start time until the loop resumes from the beginning")]
    private float cycleBuffer;

    /*
     * PRIVATE DATA
     */ 
    private float timePerCycle;
    private bool runningTasks;

    /*
     * PUBLIC INTERFACE
     */ 

    public void StartRunningTasks()
    {
        runningTasks = true;
        StartCoroutine("RunTasks");
    }

    public void StopRunningTasks()
    {
        runningTasks = false;
        StopAllCoroutines();
    }

    /*
     * PRIVATE HELPERS
     */ 

    private void Start()
    {
        timePerCycle = LatestPossibleStartTime() + cycleBuffer;
        if (beginOnStart)
        {
            StartRunningTasks();
        }
    }

    private IEnumerator RunTasks()
    {
        float currentCycleRuntime;
        WaitForSeconds refreshInterval = new WaitForSeconds(REFRESH_INTERVAL);

        // Loop while running scheduled tasks
        while (runningTasks)
        {
            currentCycleRuntime = 0f;
            StopTasks();

            // Central cycle loop
            while (currentCycleRuntime <= timePerCycle)
            {
                CheckAndStartTasks(currentCycleRuntime);
                yield return refreshInterval;
                currentCycleRuntime += REFRESH_INTERVAL;
            }
        }
    }

    // Reset the tasks in preparation for another task run cycle
    private void StopTasks()
    {
        foreach (Task task in tasks)
        {
            task.StopTask();
        }
    }
    // Run any task in the list that has not been started
    private void CheckAndStartTasks(float currentTime)
    {
        foreach (Task task in tasks)
        {
            // If the task is set to start now and hasn't already been started,
            // run the task
            if (Mathf.Abs(currentTime - task.startTime) < 0.005f && !task.started)
            {
                task.StartTask();
            }
        }
    }

    private float LatestPossibleStartTime()
    {
        if (tasks.Count > 0)
        {
            float latestStartTime = tasks[0].LatestPossibleStartTime();
            foreach (Task task in tasks)
            {
                if (task.LatestPossibleStartTime() > latestStartTime)
                {
                    latestStartTime = task.LatestPossibleStartTime();
                }
            }
            return latestStartTime;
        }
        else
        {
            return 0f;
        }
    }
}
