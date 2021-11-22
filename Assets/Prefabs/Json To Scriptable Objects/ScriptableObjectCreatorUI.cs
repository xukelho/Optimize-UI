using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(JsonToScriptableObjects))]
public class ScriptableObjectCreatorUI : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        JsonToScriptableObjects converter = (JsonToScriptableObjects)target;

        if (GUILayout.Button("Create Objects from JSON"))
            converter.CreateScriptableObjectsFromJson();
    }
}