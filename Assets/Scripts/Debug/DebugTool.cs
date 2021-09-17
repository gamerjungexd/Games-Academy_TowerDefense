using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DebugTool : MonoBehaviour
{
    public void ChangeTimescale(float value)
    {
        Time.timeScale = value;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(DebugTool))]
public class DebugToolEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Time Scale: 0"))
        {
            ((DebugTool)target).ChangeTimescale(0f);
        }
        if (GUILayout.Button("Time Scale: 1"))
        {
            ((DebugTool)target).ChangeTimescale(1f);
        }
        if (GUILayout.Button("Time Scale: 3"))
        {
            ((DebugTool)target).ChangeTimescale(3f);
        }
        EditorGUILayout.EndHorizontal();
        base.OnInspectorGUI();
    }
}
#endif



