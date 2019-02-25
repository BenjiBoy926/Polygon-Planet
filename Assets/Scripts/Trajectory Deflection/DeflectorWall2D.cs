using UnityEngine;

/*
 * CLASS DeflectorWall2D
 * ---------------------
 * A single wall that deflects the trajectory of any rigidbody
 * entering its trigger
 * ---------------------
 */ 

public class DeflectorWall2D : MonoBehaviour
{
    [SerializeField]
    private Vector2 baseNormal; // Vector perpendicular to the surface of the wall before any rotation is applied
    private Vector2 trueNormal = new Vector2(); // True normal after accounting for the wall's root global rotation

    // Calculate the true normal according to the z-rotation of the object
    private void Start()
    {
        trueNormal = baseNormal.RotatedVector(transform.root.rotation.eulerAngles.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the collider has a rigidbody...
        if(collision.attachedRigidbody != null)
        {
            //...reflect its velocity off of the wall's vector
            collision.attachedRigidbody.velocity = Vector2.Reflect(collision.attachedRigidbody.velocity, trueNormal);
        }
    }
}
