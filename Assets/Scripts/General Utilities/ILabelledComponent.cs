using UnityEngine;
using System.Collections;

// Generic interface describing any component that 
// has some string label associated with it
public interface ILabelledComponent
{
    string label { get; }
}
