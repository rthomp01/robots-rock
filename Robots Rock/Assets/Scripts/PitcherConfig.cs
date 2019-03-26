using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PitcherConfig", menuName = "Configuration/Pitcher")]
public class PitcherConfig : ScriptableObject
{
    public float pitchFrequency;
    public float pitchSpeed;
}
