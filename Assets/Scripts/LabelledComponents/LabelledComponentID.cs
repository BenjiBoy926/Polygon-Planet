using UnityEngine;

public class LabelledComponentID
{
    [SerializeField]
    [Tooltip("Tag of the game object that the labelled component should be found on")]
    private string _gameObjectTag;
    public string gameObjectTag { get { return _gameObjectTag; } }

    [SerializeField]
    [Tooltip("Label of the component that should be found on the corresponding game object")]
    private string _labelledComponentTag;
    public string labelledComponentTag { get { return _labelledComponentTag; } }
}
