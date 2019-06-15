using UnityEngine;
using System.Collections.Generic;

public class LabelledComponentKeeper<T> where T : ILabelledComponent
{
    /*
     * PUBLIC TYPEDEFS
     */
    [System.Serializable]
    public class TagPair
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

    [SerializeField]
    [Tooltip("List of pairs of labels for game objects " +
        "and labels for the components to grab off of them")]
    private List<TagPair> tagPairs;
    public List<T> components { get; private set; }

    public LabelledComponentKeeper()
    {
        foreach (TagPair pair in tagPairs)
        {
            // Use utility to get all labelled components
            T[] array = LabelledComponentUtility.FindComponentsWithLabel<T>(pair.gameObjectTag, pair.labelledComponentTag);

            // Add all found components to the list
            foreach(T component in array)
            {
                components.Add(component);
            }
        }
    }
}
