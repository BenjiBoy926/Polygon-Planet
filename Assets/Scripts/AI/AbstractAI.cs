using System;
using UnityEngine;

[Serializable]
public abstract class AbstractAI<ExternalData, InternalData, DecisionData, Apparatus>
{
    [SerializeField]
    [Tooltip("The data that the AI knows about the environment")]
    protected ExternalData sensoryData;
    [SerializeField]
    [Tooltip("The data that the AI knows about itself")]
    protected InternalData stats;
    [SerializeField]
    [Tooltip("The data that the AI uses to make decisions")]
    protected DecisionData decisions;
    [SerializeField]
    [Tooltip("The data that the AI uses to affect the environment")]
    protected Apparatus parts;

    // Scan the environment and update the AI's awareness of the environment
    public abstract void Scan();

    // Get the decisions that the AI will make 
    // based on its knowledge of itself and the environment
    public abstract void Decide();

    // Cause the AI to activate it's apparatus 
    // based on the current decisions it wants to make
    public abstract void Act(); 
}
