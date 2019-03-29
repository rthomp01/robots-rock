using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraShakeConfig", menuName = "Configuration/CameraShake")]
public class CameraShakeConfig : ScriptableObject
{
    public float duration = 1.0f;
    public float xIntensity = 1.0f;
    public float yIntensity = 1.0f;
}
