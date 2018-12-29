using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugScript : MonoBehaviour
{
    private void Start()
    {
        Timekeeper.instance.ScaledMoment(3f, 0.1f);
    }
}
