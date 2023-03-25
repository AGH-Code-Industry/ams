using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class ScriptableObjectIdAttribute : PropertyAttribute { }

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ScriptableObjectIdAttribute))]
public class ScriptableObjectIdDrawer : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        bool previousEnableState = GUI.enabled;
        
        GUI.enabled = false;
        if (string.IsNullOrEmpty(property.stringValue)) {
            property.stringValue = System.Guid.NewGuid().ToString();
        }
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = previousEnableState;
    }
}
#endif

public class ScriptableObjectWithId : ScriptableObject {
    [ScriptableObjectId]
    public string Id;

    [ContextMenu("Regenerate Id")]
    void RegenerateId() {
        Id = System.Guid.NewGuid().ToString();
    }
}