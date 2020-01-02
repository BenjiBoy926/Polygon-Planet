using UnityEngine;

/*
 * CLASS Boundary : MonoBehaviour
 * ------------------------------
 * Defines a boundary that disables any object
 * exiting its trigger
 * ------------------------------
 */ 

public class Boundary : MonoBehaviour 
{
    public Bounds bounds { get; private set; }

    private void Start()
    {
        bounds = GetComponent<Collider2D>().bounds;
    }

    // Disables any object exiting its trigger
    private void OnTriggerExit2D (Collider2D other)
	{
        other.gameObject.SetActive(false);
	} // END method
} // END class
