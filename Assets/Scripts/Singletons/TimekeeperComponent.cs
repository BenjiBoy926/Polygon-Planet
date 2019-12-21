using UnityEngine;

public class TimekeeperComponent : MonoBehaviour
{
    public float alteredTimescale;
    public float timescaleAlterTime;

    public void RestoreNormalTimescale()
    {
        Timekeeper.instance.RestoreNormalTimescale();
    }
    public void PauseGame(bool isPausing)
    {
        Timekeeper.instance.PauseGame(isPausing);
    }
    public void ScaledMoment()
    {
        Timekeeper.instance.ScaledMoment(alteredTimescale, timescaleAlterTime);
    }
}
