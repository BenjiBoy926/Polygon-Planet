using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(MonoBehaviourEvents))]
[CanEditMultipleObjects]
public class MonoBehaviourEventsEditor : Editor
{
    private string[] propertyNames =
    {
        "basic", "_start", "_awake", "_update", "_fixedUpdate", "_lateUpdate", "_onEnable", "_onDisable", "_onDestroy",
        "collision", "_onCollisionEnter", "_onCollisionStay", "_onCollisionExit", "_onTriggerEnter", "_onTriggerStay", "_onTriggerExit", "_onControllerColliderHit",
        "collision2D", "_onCollisionEnter2D", "_onCollisionStay2D", "_onCollisionExit2D", "_onTriggerEnter2D","_onTriggerStay2D","_onTriggerExit2D",
        "transforms", "_onTransformChildrenChanged", "_onTransformParentChanged",
        "editor", "_onInitialized", "_onValidate", "_reset", "_onBecameVisible", "_onBecameInvisibe",
        "gui", "_onGUI", "_onDrawGizmos", "_onDrawGizmosSelected", "_onPreRender", "_onPostRender", "_onPreCull", "_onRenderImage", "_onRenderObject", "_onWillRenderObject", "_onParticleCollision", "_onParticleSystemStopped", "_onParticleTrigger",
        "application", "_onApplicationFocus", "_onApplicationPause", "_onApplicationQuit",
        "mouse", "_onMouseDown", "_onMouseDrag", "_onMouseEnter", "_onMouseExit", "_onMouseOver", "_onMouseUp", "_onMouseUpAsButton",
        "animator", "_onAnimatorIK", "_onAnimatorMove", "_onJointBreak", "_onJointBreak2D",
        "misc", "_onAudioFilterRead", "_onConnectedToServer"
    };

    private SerializedPropertyDictionary serializedProperties;

    private void OnEnable()
    {
        serializedProperties = new SerializedPropertyDictionary(serializedObject, propertyNames);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SerializeSelectProperties("basic", "collision");
        SerializeSelectProperties("collision", "collision2D");
        SerializeSelectProperties("collision2D", "transforms");
        SerializeSelectProperties("transforms", "editor");
        SerializeSelectProperties("editor", "gui");
        SerializeSelectProperties("gui", "application");
        SerializeSelectProperties("application", "mouse");
        SerializeSelectProperties("mouse", "animator");
        SerializeSelectProperties("animator", "misc");
        SerializeSelectProperties("misc", "end");

        serializedObject.ApplyModifiedProperties();
    }

    private void SerializeSelectProperties(string boolPropertyName, string boundingPropertyName)
    {
        // Layout the field for the boolean property
        EditorGUILayout.PropertyField(serializedProperties[boolPropertyName]);

        if(serializedProperties[boolPropertyName].boolValue)
        {
            // Find the position in the list where the bool property name begins
            List<string> properties = propertyNames.ToList();
            int startIndex = properties.FindIndex(boolPropertyName.Equals);

            // Show all properties after the bool property up to the property named "boundingPropertyName",
            // or until there are no properties left to display
            for(int i = startIndex + 1; propertyNames[i] != boundingPropertyName && i < propertyNames.Length; i++)
            {
                EditorGUILayout.PropertyField(serializedProperties[propertyNames[i]]);
            }
        }
    }
}
