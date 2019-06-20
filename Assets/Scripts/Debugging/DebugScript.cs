using UnityEngine;
using System.Collections;

public class DebugScript : MonoBehaviour
{
    public SupplySweepingVector2D vectorSupplier;
    public int numLoops;

    private void Start()
    {
        StartCoroutine("RotateBySuppliedVector");
    }

    private IEnumerator RotateBySuppliedVector()
    {
        for (int i = 0; i < numLoops; i++)
        {
            transform.LookInDirection2D(vectorSupplier.Supply(), Vector2.right);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
