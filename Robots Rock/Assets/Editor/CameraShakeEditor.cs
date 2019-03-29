using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraShake))]
public class CameraShakeEditor : Editor
{
    CameraShakeConfig debugConfig;

    float debug_xShake = 1.0f;
    float debug_yShake = 1.0f;
    float debugDuration = 1.0f;

    private void OnEnable()
    {
        debugConfig = ScriptableObject.CreateInstance<CameraShakeConfig>();
    }

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
            debugDuration = EditorGUILayout.FloatField("Test Duration:", debugDuration);
            debug_xShake = EditorGUILayout.FloatField("Test xShake:", debug_xShake);
            debug_yShake = EditorGUILayout.FloatField("Test yShake:", debug_yShake);

            if (GUILayout.Button("Test Shake"))
            {
                debugConfig.duration = debugDuration;
                debugConfig.xIntensity = debug_xShake;
                debugConfig.yIntensity = debug_yShake;

                thisScript.ShakeCamera(debugConfig);
            }
        }
        EditorGUI.indentLevel--;
    }
}
