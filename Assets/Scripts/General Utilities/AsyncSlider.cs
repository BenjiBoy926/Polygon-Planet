using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AsyncSlider : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The GUI slider that will display the progress of the scene loading")]
    private Slider progressBar;

    private AsyncOperation currentOperation;

    public void StartAsyncSlider(AsyncOperation operation)
    {
        currentOperation = operation;

        StopAllCoroutines();
        StartCoroutine("UpdateAsyncSlider");
    }
    public void StopAsyncSlider()
    {
        StopAllCoroutines();
    }
    public void ResetAsyncSlider()
    {
        progressBar.value = progressBar.minValue;
    }

    private IEnumerator UpdateAsyncSlider()
    {
        float magnitude = progressBar.maxValue - progressBar.minValue;
        ResetAsyncSlider();

        while(!currentOperation.isDone)
        {
            progressBar.value = progressBar.minValue + (magnitude * currentOperation.progress);
            yield return null;
        }
    }
}
