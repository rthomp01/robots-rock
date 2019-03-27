using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class OutlineShaderGUI : ShaderGUI
{
    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        //Texture and Color Settings
        MaterialProperty _MainTex = ShaderGUI.FindProperty("_MainTex", properties);
        MaterialProperty _Color = ShaderGUI.FindProperty("_Color", properties);
        materialEditor.ShaderProperty(_MainTex, new GUIContent(_MainTex.displayName, "Main diffuse(RGB) texture."));
        materialEditor.ShaderProperty(_Color, new GUIContent(_Color.displayName, "A tint color (RGB) to be multiplied against the main texture."));
        EditorGUILayout.Space();

        //Outline Specific Options
        EditorGUILayout.LabelField("Outline Options", EditorStyles.boldLabel);
        EditorGUI.indentLevel++;
        MaterialProperty _Outline = ShaderGUI.FindProperty("_Outline", properties);
        materialEditor.ShaderProperty(_Outline, new GUIContent(_Outline.displayName, "Toggle whether or not the outline should be drawn."));
        if (_Outline.floatValue == 1)
        {
            MaterialProperty _OutlineWidth = ShaderGUI.FindProperty("_OutlineWidth", properties);
            MaterialProperty _OutlineColor = ShaderGUI.FindProperty("_OutlineColor", properties);
            materialEditor.ShaderProperty(_OutlineWidth, new GUIContent(_OutlineWidth.displayName, "How thick the outline will be drawn.")); //_OutlineWidth.displayName);
            materialEditor.ShaderProperty(_OutlineColor, new GUIContent(_OutlineColor.displayName, "The color the outline will be drawn. Supports transparency via alpha channel."));
        }
        EditorGUI.indentLevel--;
        EditorGUILayout.Space();
    }
}
