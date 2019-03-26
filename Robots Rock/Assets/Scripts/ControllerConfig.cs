using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ControllerConfig", menuName = "Configuration/Controller")]
public class ControllerConfig : ScriptableObject
{
    public float walkSpeed = 3.0f;
    public float airborneSpeed = 1.75f;
    public float turnRate = 720.0f;
    public float inputDampening = 0.1f;

}
