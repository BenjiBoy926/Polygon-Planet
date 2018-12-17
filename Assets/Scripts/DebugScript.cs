using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
    private State state = new State(10f);

    private void Start()
    {
        StartCoroutine("TestStateThreading");
    }

    IEnumerator TestStateThreading()
    {
        state.Activate(10f);
        Debug.Log("Iterator starts at realtime " + Time.unscaledTime);

        yield return new WaitForSeconds(5f);

        Timekeeper.PauseGame(true);
        Debug.Log("The game pauses at realtime " + Time.unscaledTime);

        yield return new WaitForSecondsRealtime(10f);

        Timekeeper.PauseGame(false);
        Debug.Log("The game resumes at realtime " + Time.unscaledTime);
    }
}
