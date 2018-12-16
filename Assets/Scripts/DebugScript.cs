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

        yield return new WaitForSeconds(5f);

        state.Deactivate();

        yield return new WaitForSeconds(1f);

        state.Activate(10f);
    }
}
