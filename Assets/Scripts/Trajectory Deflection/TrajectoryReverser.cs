using UnityEngine;
using System.Collections;

public class TrajectoryReverser : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Amount by which the collision object is scaled when it collides with this object")]
    private float speedScalar;

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.rigidbody;
        if (rb != null)
        {
            rb.velocity *= -speedScalar;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.rigidbody;
        if (rb != null)
        {
            rb.velocity *= -speedScalar;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null)
        {
            rb.velocity *= -speedScalar;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.attachedRigidbody;
        if (rb != null)
        {
            rb.velocity *= -speedScalar;
        }
    }
}
