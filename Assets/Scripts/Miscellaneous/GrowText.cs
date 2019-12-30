using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GrowText : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The text script that will grow in size")]
    private Text text;
    [SerializeField]
    [Tooltip("Start size of the text")]
    private int startSize;
    [SerializeField]
    [Tooltip("End size of the text")]
    private int endSize;
    [SerializeField]
    [Tooltip("Time it takes to transition from the start to end size")]
    private float time;

    private float inverseTime;

    private void Awake()
    {
        inverseTime = 1f / time;
    }

    public void Grow()
    {
        StopAllCoroutines();
        StartCoroutine("GrowOverTime");
    }

    private IEnumerator GrowOverTime()
    {
        text.fontSize = startSize;
        while (Mathf.Abs(text.fontSize - endSize) > 5)
        {
            text.fontSize = Mathf.RoundToInt(Mathf.Lerp((float)text.fontSize, (float)endSize, Time.deltaTime * inverseTime));
            yield return null;
        }
    }
}
