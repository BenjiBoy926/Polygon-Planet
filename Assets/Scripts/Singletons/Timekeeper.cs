using System.Collections;
using UnityEngine;

/*
 * CLASS Timekeeper
 * ----------------
 * Keeps track of time.  In this project, it pauses and resumes the game
 * ----------------
 */
public class Timekeeper : MonoSingleton<Timekeeper>
{
	private bool _paused = false;	// True if the game is currently paused
	public bool paused { get { return _paused; } }

    private float _timeScale;

    // Member variable gives threads other than the main thread access to the current time scale
    // as long as any modifications of the time scale go through this property
    public float timeScale
    {
        get
        {
            return _timeScale;
        }
        set
        {
            _timeScale = value;
            Time.timeScale = value;
        }
    }

    // Create an instance of the singleton as soon as the program starts running
    [RuntimeInitializeOnLoadMethod]
    private static void CreateInstance()
    {
        BaseCreateInstance("Timekeeper");
    }

    private void Start()
    {
        _timeScale = Time.timeScale;
    }

    // Restores the normal timescale
    // Additionally stops any coroutines or invokes
    public void RestoreNormalTimescale()
    {
        StopAllCoroutines();
        timeScale = 1f;
    }

    // Pause the game by setting the timescale to zero,
    // or unpause by setting it back to normal
    public void PauseGame (bool isPausing)
	{
		// Set timescale to zero or one, depending on whether we're pausing
		if (isPausing) {
			timeScale = 0f;
		} else {
			timeScale = 1f;
		}

		// Set local variable
		_paused = isPausing;
	}

    // Causes the game to run at the given timescale for the given amount of time in realtime
    public void ScaledMoment(float realtime, float newScale)
    {
        timeScale = newScale;
        StopAllCoroutines();
        StartCoroutine("RestoreTimescaleAfterRealtime", realtime);
    }

    // Waits the given time in realtime, then restores the normal timescale
    private IEnumerator RestoreTimescaleAfterRealtime(float realtime)
    {
        yield return new WaitForSecondsRealtime(realtime);
        RestoreNormalTimescale();
    }
}
