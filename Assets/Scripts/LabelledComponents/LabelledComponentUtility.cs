using UnityEngine;
using System;
using System.Collections.Generic;

/*
 * CLASS ComponentUtility
 * ----------------------
 * Utility for certain custom components with similar qualities
 * Currently allows grabbing 
 * ----------------------
 */ 

public static class LabelledComponentUtility
{
    // Get a list of components with the given label on all game objects with the given tags
    public static T[] FindComponentsWithLabel<T>(string gObjectTag, string componentLabel, bool includeChildren = false)
        where T : ILabelledComponent
    {
        GameObject[] gObjects = GameObject.FindGameObjectsWithTag(gObjectTag);
        return FindComponentsWithLabel<T>(gObjects, componentLabel, includeChildren);
    }
    // Get a list of all the components with the given label on all the given game objects
    public static T[] FindComponentsWithLabel<T>(GameObject[] gObjects, string componentLabel, bool includeChildren = false)
        where T : ILabelledComponent
    {
        T[] tempComponents;    // Temporary storage for the current stockpile the algorithm is looking for
        List<T> components = new List<T>(); // List of stockpiles to return
        // Loop through each game object and try to grab a stockpile with the given tag
        foreach (GameObject obj in gObjects)
        {
            // Find and add all components with the label on the current game object
            tempComponents = FindComponentsWithLabel<T>(obj, componentLabel);
            if (tempComponents != null)
            {
                components.AddRange(tempComponents);
            }
        }
        return components.ToArray();
    }
    // Find all components with the given label on the single game object given
    public static T[] FindComponentsWithLabel<T>(GameObject gObject, string componentLabel, bool includeChildren = false)
        where T : ILabelledComponent
    {
        List<T> list = ListLabelledComponents<T>(gObject, includeChildren);
        Predicate<T> matchedTag = (x) => x.label == componentLabel;
        return list.FindAll(matchedTag).ToArray();
    }
    // Find a component with the given label on the GameObject with the given tag
    public static T FindComponentWithLabel<T>(string gObjectTag, string componentLabel)
        where T : ILabelledComponent
    {
        GameObject gObject = GameObject.FindGameObjectWithTag(gObjectTag);
        return FindComponentWithLabel<T>(gObject, componentLabel);
    }
    // Find a component with the given label on the given game object
    public static T FindComponentWithLabel<T>(GameObject gObject, string componentLabel, bool includeChildren = false)
        where T : ILabelledComponent
    {
        // Get all stockpiles in this object and the objects children
        List<T> list = ListLabelledComponents<T>(gObject, includeChildren);
        // Find a stockpile with the same tag given
        Predicate<T> matchedTag = (x) => x.label == componentLabel;
        return list.Find(matchedTag);
    }
    private static List<T> ListLabelledComponents<T>(GameObject gObject, bool includeChildren)
        where T : ILabelledComponent
    {
        if(includeChildren)
        {
            return ListLabelledComponentsHere<T>(gObject);
        }
        else
        {
            return ListLabelledComponentsInChildren<T>(gObject);
        }
    }
    private static List<T> ListLabelledComponentsHere<T>(GameObject gObject)
        where T : ILabelledComponent
    {
        T[] components = gObject.GetComponents<T>();
        return new List<T>(components);
    }
    // Create a list of the labelled components in the given game object
    private static List<T> ListLabelledComponentsInChildren<T>(GameObject gObject)
        where T : ILabelledComponent
    {
        T[] components = gObject.GetComponentsInChildren<T>();
        return new List<T>(components);
    }
}
