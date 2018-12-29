using UnityEngine;
using System.Collections;

/*
 * CLASS BlowbackObject2D
 * ----------------------
 * Blowback objects cause blowback movers entering their trigger
 * to blowback away from them
 * ----------------------
 */ 

public class BlowbackObject2D : MonoBehaviour
{
    [SerializeField]
    private float blowbackStrength; // Strength of the blowback given to the object that hits this trigger
    private BlowbackMover2D recentlyHit;    // Blowback mover on the object hit

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        // If the object hit has a blowback mover on it, cause it to experience blowback away from the explosion
        recentlyHit = collision.GetComponent<BlowbackMover2D>();
        if (recentlyHit != null)
        {
            recentlyHit.BlowbackFromPoint(transform.position, blowbackStrength);
        }
    }
}
