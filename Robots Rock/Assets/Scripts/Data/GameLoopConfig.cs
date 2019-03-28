using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameLoopConfig", menuName = "Configuration/GameLoop")]
public class GameLoopConfig : ScriptableObject
{
    public float titlePreActiveDelay;
    public float readyWaitDelay;
    public float goMessageActiveDelay;
    public float gameOverMessageDelay;
}
