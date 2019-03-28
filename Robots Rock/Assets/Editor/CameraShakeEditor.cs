using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraShake))]
public class CameraShakeEditor : Editor
{
    float debugMagnitude;
    float debugDuration;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CameraShake thisScript = (CameraShake)target;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField(new GUIContent("Debug Settings (Visible in Play Mode):",
            "When in play mode, used to test the camera shake function with different parameters.")
            , EditorStyles.label);

        EditorGUI.indentLevel++;
        if (Application.isPlaying)
        {
            debugMagnitude = EditorGUILayout.FloatField("Test Magnitude:", debugMagnitude);
            debugDuration = EditorGUILayout.FloatField("Test Duration:", debugDuration);

            if (GUILayout.Button("Test Shake"))
            {
                thisScript.ShakeCamera(debugMagnitude, debugDuration);
            }
        }
        EditorGUI.indentLevel--;
    }
}
