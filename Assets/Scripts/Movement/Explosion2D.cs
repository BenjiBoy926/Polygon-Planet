using UnityEngine;
using System.Collections;

/*
 * CLASS Explosion2D
 * -----------------
 * A type of blowback object with the additional ability
 * to be released for a short amount of time and quickly deactivated
 * -----------------
 */ 

public class Explosion2D : BlowbackObject2D
{
    private const float LINGER_TIME = 0.1f; // Time for which the explosion's hitbox lingers before dissipating

    // Move the explosion and activate it
    public void ExplodeAtPoint(Vector2 point)
    {
        transform.position = new Vector3(point.x, point.y, transform.position.z);
        gameObject.SetActive(true);

        // Disable the explosion after it lingers for the appropriate amount of time
        CancelInvoke();
        Invoke("Disable", LINGER_TIME);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}
