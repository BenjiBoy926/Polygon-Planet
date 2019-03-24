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

public static class ComponentUtility
{
    // Get a list of components with the given label on all game objects with the given tags
    public static T[] FindComponentsWithLabel<T>(string gObjectTag, string componentLabel)
        where T : Component, ILabelledComponent
    {
        GameObject[] gObjects = GameObject.FindGameObjectsWithTag(gObjectTag);
        return FindComponentsWithLabel<T>(gObjects, componentLabel);
    }
    // Get a list of all the components with the given label on all the given game objects
    public static T[] FindComponentsWithLabel<T>(GameObject[] gObjects, string componentLabel)
        where T : Component, ILabelledComponent
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
    public static T[] FindComponentsWithLabel<T>(GameObject gObject, string componentLabel)
        where T : Component, ILabelledComponent
    {
        List<T> list = ListLabelledComponentsInChildren<T>(gObject, componentLabel);
        Predicate<T> matchedTag = (x) => x.label == componentLabel;
        return list.FindAll(matchedTag).ToArray();
    }
    // Find a component with the given label on the GameObject with the given tag
    public static T FindComponentWithLabel<T>(string gObjectTag, string componentLabel)
        where T : Component, ILabelledComponent
    {
        GameObject gObject = GameObject.FindGameObjectWithTag(gObjectTag);
        return FindComponentWithLabel<T>(gObject, componentLabel);
    }
    // Find a component with the given label on the given game object
    public static T FindComponentWithLabel<T>(GameObject gObject, string componentLabel)
        where T : Component, ILabelledComponent
    {
        // Get all stockpiles in this object and the objects children
        List<T> list = ListLabelledComponentsInChildren<T>(gObject, componentLabel);
        // Find a stockpile with the same tag given
        Predicate<T> matchedTag = (x) => x.label == componentLabel;
        return list.Find(matchedTag);
    }
    // Create a list of the labelled components in the given game object
    private static List<T> ListLabelledComponentsInChildren<T>(GameObject gObject, string componentLabel)
        where T : Component, ILabelledComponent
    {
        T[] components = gObject.GetComponentsInChildren<T>();
        return new List<T>(components);
    }
}
